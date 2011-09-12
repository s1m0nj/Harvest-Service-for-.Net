using System;

namespace HarvestApi.Service.Request
{
    internal class HttpMethod
    {
        public string Verb { get; private set; }
        private HttpMethod(string verb){Verb = verb;}

        internal static HttpMethod Get = new HttpMethod(System.Net.WebRequestMethods.Http.Get);
        internal static HttpMethod Put = new HttpMethod(System.Net.WebRequestMethods.Http.Put);
        internal static HttpMethod Post = new HttpMethod(System.Net.WebRequestMethods.Http.Post);
        internal static HttpMethod Delete = new HttpMethod( "DELETE");
    }
}