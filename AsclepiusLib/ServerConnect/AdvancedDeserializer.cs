using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace ServerConnect
{
    public class AdvancedDeserializer : IDeserializer
    {
        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(response.Content);
            else
                return default(T);
        }
    }
}
