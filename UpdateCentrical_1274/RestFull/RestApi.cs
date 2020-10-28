using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UpdateCentrical_1274.RestFull
{
    public class RestApi
    {
        private readonly ILog log = LogManager.GetLogger(typeof(RestApi));
        private string _CentricalUrl = null;
        private string _username = null;
        private string _password = null;
        private readonly string _APIKey = ConfigurationManager.AppSettings["APIKey"];
        private static RestApi m_instance;
        public RestApi(string baseURL)
        {
           
            _CentricalUrl = baseURL;
        }
        private RestApi()
        {
            _CentricalUrl = ConfigurationManager.AppSettings["CentricalUrl"];
            _username = ConfigurationManager.AppSettings["userName"];
            _password = ConfigurationManager.AppSettings["password"];
            log.Debug($"Web Service URL: {_CentricalUrl}");
        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static RestApi Instance()
        {
            if (m_instance == null)
            {
                m_instance = new RestApi();
            }
            return m_instance;
        }

        public string POST(string json)
        {
            log.Debug($"Start POST to URL: {_CentricalUrl} with json: {json}");
            //log start POST
            string result;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_CentricalUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("x-api-key", _APIKey);
                httpWebRequest.Credentials = new NetworkCredential(_username, _password);
                httpWebRequest.Proxy.Credentials = new NetworkCredential(_username, _password);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    log.Debug($"Result: {result}");
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                log.Error(result);
            }
            return result;

        }

       

      




    }
}
