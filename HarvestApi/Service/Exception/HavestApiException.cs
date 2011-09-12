using System.Net;

namespace HarvestApi.Service.Exception
{
    public class HavestApiException : System.Exception
    {
        public HavestApiException(string url, string httpMethod, string content,
                                  string statusDescription, HttpStatusCode statusCode, HttpStatusCode httpStatusCode)
            : base(
                string.Format(
                    "The server returned '{0}' with the status code {1} ({2:d}).\r\nUrl:{3}\r\nMethod:{4}\r\nContent:{5}",
                    statusDescription, statusCode, httpStatusCode, url, httpMethod, content))
        {
            Url = url;
            HttpMethod = httpMethod;
            Content = content;
            StatusDescription = statusDescription;
            StatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
        }

        public string Url { get; private set; }
        public string HttpMethod { get; private set; }
        public string Content { get; private set; }
        public string StatusDescription { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
    }
}