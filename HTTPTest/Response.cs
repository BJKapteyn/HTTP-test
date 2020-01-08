using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPTest
{
    public class Response
    {
        private Byte[] data = null;
        private string status;
        private string mime;
        private Response(string status, string mime, Byte[] data)
        {
            this.mime = mime;
            this.status = status;
            this.data = data;
        }
        //left off here
        public static Response From(Request request)
        {
            if(request == null)
            {
                return MakeNullRequest();
            }
            if(request.Type == "GET")
            {
                string file = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
                FileInfo f = new FileInfo(file);
                if(f.Exists && f.Extension.Contains("."))
                {

                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo
                }
            }
            else
            {
                return MakeNotAllowedRequest();
            }
        }

        private static Response MakeNotAllowedRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, (int)fileStream.Length);

            return new Response("405 Not allowed", "text/html", new Byte[0]);
        }
        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, (int)fileStream.Length);

            return new Response("400 Bad Request", "text/html", new Byte[0]);
        }
        private static Response MakeBadRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, (int)fileStream.Length);

            return new Response("404 Page not found", "text/html", new Byte[0]);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine($"{HTTPServer.Version} {status}\r\nServer {HTTPServer.Name}\r\nContent-Type: {mime}" +
                $"\r\nAccept-Ranges: bytes\r\nContent-Length: {data.Length}");


            stream.Write(data, 0, data.Length);
        }

    }
}
