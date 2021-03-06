﻿using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using HarvestApi.Service.Exception;

namespace HarvestApi.Service.Request
{
    internal abstract class AbstractService : IServiceCommand

    {
        private readonly HarvestConnection _harvestConnection;
        private readonly string _endpoint;
        private int _retriedAfterSeconds;
        
        /// <summary>
        /// Indicates the seconds the Harvest server requested that we wait in between
        /// </summary>
        public int HavestRequestForcedWaitForApiThrotterling
        {
            get { return _retriedAfterSeconds; }
        }
    

        protected AbstractService(HarvestConnection harvestConnection, string endpoint)
        {
            _harvestConnection = harvestConnection;
            _endpoint = endpoint;
        }

        protected AbstractService(HarvestConnection harvestConnection, string endpointFormatString, params object[] args)
            : this(harvestConnection, string.Format(endpointFormatString, HarvestEncode(args)))
        {
        }

        protected abstract HttpMethod HttpMethod { get; }
        internal string Content = string.Empty;

        public string Exectue(string xmlContent="")
        {
            Content = xmlContent;
            _retriedAfterSeconds = 0;
            return ExecuteRequestAndRetryIfServiceUnavailable();
        }

        private string ExecuteRequestAndRetryIfServiceUnavailable()
        {
            try
            {
                return ExecuteRequest();
            }
            catch (HarvestApiException harvestEx)
            {
                if (harvestEx.StatusCode==HttpStatusCode.ServiceUnavailable && harvestEx.RetryAfterSeconds > 0)
                {
                    _retriedAfterSeconds = harvestEx.RetryAfterSeconds;
                    Thread.Sleep(_retriedAfterSeconds * 1000);
                    return ExecuteRequest();
                    
                }
                throw;
            }
        }

        private string ExecuteRequest()
        {
            string endpointUri = _harvestConnection.Uri + _endpoint;
            HttpWebResponse response = null;
            try
            {
                //Setup request and authentication
                HttpWebRequest request = RequestFactory(endpointUri);

                using (response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                        var sbSource = new StringBuilder(reader.ReadToEnd());
                        return sbSource.ToString();
                    }
                }
                return string.Empty;
            }
            catch (WebException wex)
            {
                if (wex.Response == null) 
                {
                    throw;
                }
                throw new HarvestApiException(endpointUri, HttpMethod.Verb, Content,wex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        private HttpWebRequest RequestFactory(string endpointUri)
        {
            HttpWebRequest request;
            ServicePointManager.ServerCertificateValidationCallback = Validator;
            request = (HttpWebRequest)WebRequest.Create(endpointUri);
            request.Method = HttpMethod.Verb;
            request.Accept = "application/xml";
            request.ContentType = "application/xml";
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;
            request.UserAgent = "HarvestServiceForDotNet";
            request.PreAuthenticate = true;

            string usernameAndPassword = string.Format("{0}:{1}", _harvestConnection.Username, _harvestConnection.Password);
            request.Headers.Add("Authorization",
                                "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernameAndPassword)));
                
            //Add request context/body
            if (string.IsNullOrEmpty(Content))
            {
                request.ContentLength = 0;
            }
            else
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Content);
                request.ContentLength = bytes.Length;
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(bytes, 0, bytes.Length);
                reqStream.Close();
            }
            return request;
        }

        /// <summary>
        /// UrlEncode seems to encode the url too much so we just use a manual encoder to their specific requirements
        /// </summary>
        private static object[] HarvestEncode(params object[] args)
        {
            var encodedArgs = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                object unencodedArg = args[i];
                object encodedArg = unencodedArg; //Default
                if (unencodedArg is DateTime)
                {
                    encodedArg = ((DateTime) unencodedArg).ToString("yyyy-MM-dd+HH:ss").Replace(":", "%3A");
                }
                encodedArgs[i] = encodedArg;
            }
            return encodedArgs;
        }

        public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
                                     SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
}
}