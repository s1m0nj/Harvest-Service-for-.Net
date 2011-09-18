using System.Net;

namespace HarvestApi.Service.Exception
{
    public class HavestApiException : WebException
    {
        
        public HavestApiException(string url, string httpMethod, string content,WebException exception)
            : base(
                string.Format(
                    "The server request failed with status '{0}'. Url:{1}\r\nMethod:{2}\r\nContent:{3}\r\nMessage:{4}",
                    exception.Status, url, httpMethod, content, exception.Message), exception)
        {
            Url = url;
            HttpMethod = httpMethod;
            Content = content;

            if (exception.Response != null)
            {
                using (var errorResponse = (HttpWebResponse) exception.Response)
                {
                    StatusDescription = errorResponse.StatusDescription;
                    StatusCode = errorResponse.StatusCode;
                    if (errorResponse.Headers["Retry-After"] != null)
                    {
                        RetryAfterSeconds = int.Parse(errorResponse.Headers["Retry-After"]);
                    }
                }
            }
        }

        public string Url { get; private set; }
        public string HttpMethod { get; private set; }
        public string Content { get; private set; }
        public string StatusDescription { get; private set; }
        public int RetryAfterSeconds { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}