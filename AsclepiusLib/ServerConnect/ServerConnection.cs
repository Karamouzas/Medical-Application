using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using ModelsLib.Generic;
using ModelsLib.RDV.Validators;
using ModelsLib.Authentication;
using System.Security.Authentication;

namespace ServerConnect
{
    public class ServerConnection : IServerConnection
    {
        #region Constants/Enums
        public static string HEADER_TOKEN_KEY = "Medl-Token";
        public static string HEADER_EXCEPTION_ASSEMBLYNAME = "AssemblyName";
        public static string HEADER_DBSELECTION = "Medl-DB";
        public enum HttpMethod
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            DELETE = 3,
            HEAD = 4,
            OPTIONS = 5,
            PATCH = 6,
            MERGE = 7,
            GETALL,
        }
        #endregion

        #region Constructors
        public ServerConnection() { }
        public ServerConnection(string token)
        {
            SessionToken = token;
        }
        #endregion

        #region Request Properties
        public string ServerPath { get; set; }
        public string SessionToken { get; set; }
        public string CustomDatabaseKey { get; set; }
        #endregion

        #region Response Properties
        public string ResponseContent { get { return resp?.Content; } }
        public HttpStatusCode? ResponseStatusCode { get { return resp?.StatusCode; } }
        #endregion

        #region Rest Declarations
        RestClient clnt;
        RestRequest req;
        RestResponseBase resp;
        #endregion

        #region Request (Sync)
        public string Request(string resource, HttpMethod method, object requestBody = null, bool disableValidation = false)
        {
            InitializeRequest(resource, method, requestBody);
            resp = (RestResponse)clnt.Execute(req);
            if (!disableValidation)
                validateResponse();

            return resp.Content;
        }
        public T Request<T>(string resource, HttpMethod method, object requestBody = null, bool disableValidation = false) where T : new()
        {
            InitializeRequest(resource, method, requestBody);
            resp = (RestResponse<T>)clnt.Execute<T>(req);

            if (!disableValidation)
                validateResponse();

            return ((RestResponse<T>)resp).Data;
        }
        #endregion

        #region Request (Async)
        public Task<T> RequestAsync<T>(string resource, HttpMethod method, CancellationToken? cancelToken = null, object requestBody = null, bool disableValidation = false)
        {
            InitializeRequest(resource, method, requestBody);
            //var resp =  clnt.ExecuteTaskAsync<T>(req, cancelToken.GetValueOrDefault()).Result;
            return clnt.ExecuteTaskAsync<T>(req, cancelToken.GetValueOrDefault())
            .ContinueWith((x) =>
            {
                try
                {
                    resp = (RestResponse<T>)x.Result;
                    if (!disableValidation)
                        validateResponse();
                    return ((RestResponse<T>)resp).Data;
                }
                catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
                {
                   throw new TaskCanceledException(x);
                }
            });
        }

        //TODO: REMOVE
        [System.Obsolete("This method is deprecated, please use Task<T> RequestAsync<T> instead.")]
        public ServerRequestAsyncHandle RequestAsync(string resource, HttpMethod method, Action<string> callback, object requestBody = null, bool disableValidation = false)
        {
            InitializeRequest(resource, method, requestBody);

            RestRequestAsyncHandle handle = clnt.ExecuteAsync(req, response =>
            {
                resp = (RestResponseBase)response;
                if (resp.ResponseStatus == ResponseStatus.Aborted)
                    return;

                if (!disableValidation)
                    validateResponse();
                callback(response.Content);
            });

            return new ServerRequestAsyncHandle(handle);
        }

        //TODO: REMOVE
        [System.Obsolete("This method is deprecated, please use Task<T> RequestAsync<T> instead.")]
        public ServerRequestAsyncHandle RequestAsync<T>(string resource, HttpMethod method, Action<T> callback, object requestBody = null, bool disableValidation = false) where T : new()
        {
            InitializeRequest(resource, method, requestBody);

            RestRequestAsyncHandle handle;
            handle = clnt.ExecuteAsync<T>(req, response =>
            {
                resp = (RestResponseBase)response;

                if (resp.ResponseStatus == ResponseStatus.Aborted)
                    return;

                if (!disableValidation)
                    validateResponse();
                callback(((RestResponse<T>)response).Data);
            });
            return new ServerRequestAsyncHandle(handle);
        }
        #endregion

        #region Initialization/Validation
        private void InitializeRequest(string resource, HttpMethod method, object requestBody)
        {
            resp = null;
            clnt = new RestClient(ServerPath);
            clnt.AddHandler("application/json", new AdvancedDeserializer());
            req = new RestRequest(resource, (RestSharp.Method)method);
            req.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            req.Timeout = 1800000;  //30 minutes

            if (requestBody != null)
                req.AddJsonBody(requestBody);

            if (!string.IsNullOrWhiteSpace(SessionToken))
                req.AddHeader(HEADER_TOKEN_KEY, SessionToken);

            if (!string.IsNullOrWhiteSpace(CustomDatabaseKey))
                req.AddHeader(HEADER_DBSELECTION, CustomDatabaseKey);
        }

        private void validateResponse()
        {
            if (resp.StatusCode == System.Net.HttpStatusCode.OK) return;
            if (resp.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                string AssemblyName = resp.Headers.FirstOrDefault(x => x.Name == HEADER_EXCEPTION_ASSEMBLYNAME)?.Value.ToString();

                if (string.IsNullOrEmpty(AssemblyName))
                    throw new MissingFieldException("Server exception handling configuration not found.", new Exception(resp.Content));

                //Type SrvrExceptionType = Assembly.GetExecutingAssembly().GetType(ExceptionTypeHeader);
                Type SrvrExceptionType = Type.GetType(AssemblyName, false, true);

                if (SrvrExceptionType == null)
                    throw new InvalidOperationException("Valid Server exception type not found.", new Exception(resp.Content));

                MethodInfo DeserializationMethod = typeof(JsonConvert)
                    .GetMethods()
                    .FirstOrDefault(mtd =>
                        mtd.Name == "DeserializeObject"
                        && mtd.IsGenericMethod == true
                        && mtd.GetParameters().Length == 1
                        && mtd.GetParameters()[0].ParameterType == typeof(string))?.MakeGenericMethod(SrvrExceptionType);

                if (DeserializationMethod == null)
                    throw new MissingMethodException("Deserialization generic method not found.", new Exception(resp.Content));

                throw DeserializationMethod.Invoke(null, new object[] { resp.Content }) as Exception;
            }
            else
                throw new Exception(string.Format("HTTP Error:{0:d} {1}.\r\nServer Path: {2}\r\nResource: {3}\r\nResponse Uri: {4}\r\nContent:{5}. {6}", 
                    resp.StatusCode, resp.StatusDescription, ServerPath, req.Resource, resp.ResponseUri, resp.Content, resp.ErrorMessage == null ? "" : resp.ErrorMessage));
        }
        #endregion
    }
}
