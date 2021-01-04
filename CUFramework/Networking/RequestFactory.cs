using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CUFramework.Networking
{
    public class RequestFactory
    {
        private string RequestURL;
        private Dictionary<string, string> RequestData;
        public RequestFactory(string RequestURL, Dictionary<string, string> data = null)
        {
            this.RequestURL = RequestURL;
            RequestData = data??new Dictionary<string, string>();
        }

        public void Add(string a, string b)
        {
            RequestData.Add(a, b);
        }

        public string SendAndGetResponse()
        {
            return GetResponse(RequestURL, RequestData);
        }

        private static string GetResponse(string url, Dictionary<string, string> data)
        {
            string postData = "";
            foreach (KeyValuePair<string, string> kvp in data)
            {
                string v1 = kvp.Key;
                string v2 = HttpUtility.UrlEncode(kvp.Value);

                postData += "&" + v1 + "=" + v2;
            }

            if (postData.StartsWith("&"))
            {
                postData = postData.Remove(0, 1);
            }

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return (responseFromServer);
        }
    }
}
