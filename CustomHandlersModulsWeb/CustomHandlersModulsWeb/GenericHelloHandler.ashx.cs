using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomHandlersModulsWeb
{
    /// <summary>
    /// Summary description for GenericHelloHandler
    /// </summary>
    public class GenericHelloHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.WriteFile("json.json");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}