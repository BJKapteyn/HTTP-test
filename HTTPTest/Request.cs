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

        private Request(string type, string url, string host)
        {
            type = Type;
            url = URL;
            host = Host;
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
            return new Request(type, url, host);
        }
    }
}
