using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPTest
{
    public class Request
    {
        public string Type { get; set; }
        public string URL { get; set; }
        public string Host { get; set; }
        public string Referrer { get; set; }

        private Request(string type, string url, string host)
        {
            if(url == "/")
            {
                url = "index.html";
            }

            Type = type;
            URL = url;
            Host = host;
        }

        public static Request GetRequest(string request)
        {

            if(string.IsNullOrEmpty(request))
                {
                    return null;
                }
            //parse the request string and grab the type url and host
            string[] tokens = request.Split(' ');
            string type = tokens[0];
            string url = tokens[1];
            string host = tokens[4];

            //add referrer later

            //string referrer = "";

            //for (int i = 0; i < tokens.Length; i++)
            //{
            //    if(tokens[i] == "Referrer:")
            //    {
            //        referrer = tokens[i + 1];
            //        break;
            //    }
            //}
            return new Request(type, url, host);
        }
    }
}
