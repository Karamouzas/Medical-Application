using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ModelsLib.ServerConnect.ServerConnection;

namespace ServerConnect
{
    public interface IServerConnection
    {
        string ServerPath { get; set; }
        string SessionToken { get; set; }
        string CustomDatabaseKey { get; set; }
        string ResponseContent { get; }
        HttpStatusCode? ResponseStatusCode { get; }
        string Request(string resource, HttpMethod method, object requestBody = null, bool disableValidation = false);
        T Request<T>(string resource, HttpMethod method, object requestBody = null, bool disableValidation = false) where T : new();
        Task<T> RequestAsync<T>(string resource, HttpMethod method, CancellationToken? cancelToken = null, object requestBody = null, bool disableValidation = false);
    }
}
