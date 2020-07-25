using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerConnect
{
    public class ServerRequestAsyncHandle
    {
        RestRequestAsyncHandle _handle;
        public ServerRequestAsyncHandle(RestRequestAsyncHandle handle)
        {
            _handle = handle;
        }

        public void Abort()
        {
            try
            {
                _handle.Abort();
            }
            catch (Exception) { }
        }
    }
}
