using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ServerConnect
{
    [Serializable()]
    public class GenericException : Exception, ISerializable
    {
        public string ServerExceptionType { get; set; }
        public Dictionary<string, string> ServerExceptionData { get; set; }
        public new string Message { get { return ServerExceptionData != null && ServerExceptionData.ContainsKey("Message") ? ServerExceptionData["Message"] : null; } }

        public GenericException() { }

        public GenericException(SerializationInfo info, StreamingContext context)
        {
            ServerExceptionType = (string)info.GetValue("ServerExceptionType", typeof(string));
            ServerExceptionData = (Dictionary<string, string>)info.GetValue("ServerExceptionData", typeof(Dictionary<string, string>));
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ServerExceptionType", ServerExceptionType);
            info.AddValue("ServerExceptionData", ServerExceptionData);
        }
    }
}
