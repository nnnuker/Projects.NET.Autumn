using System;
using System.Web;

namespace CustomHandlersModulsWeb
{
    public class HelloModule : IHttpModule
    {
        private int counter;

        public void Init(HttpApplication app)
        {
            app.EndRequest += Show;
        }

        private void Show(object src, EventArgs args)
        {
            counter++;
            HttpContext.Current.Response.Write($"<p>Number of requests {counter}</p>");
        }

        public void Dispose()
        {

        }
    }
}
