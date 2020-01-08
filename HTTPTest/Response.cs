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
        //return the different responses based on request
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
                    DirectoryInfo directoryInfo = new DirectoryInfo(f + "/");
                    FileInfo[] files = directoryInfo.GetFiles();
                    if (!directoryInfo.Exists)
                    {
                        return MakeBadRequest();
                    }

                    foreach (FileInfo fi in files)
                    {
                        string name = fi.Name;

                        if (name.Contains("default.html") || name.Contains("default.htm") || name.Contains("index.htm") || name.Contains("index.html"))
                        {
                            f = fi;
                        }
                        return MakeFromFile(fi);
                    }
                }

                if(!f.Exists)
                {
                    return MakeBadRequest();
                }
            }
            else
            {
                return MakeNotAllowedRequest();
            }

            return MakeBadRequest();
        }

        private static Response MakeFromFile(FileInfo f)
        {
            
            FileStream fileStream = f.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, (int)fileStream.Length);
            fileStream.Close();
            return new Response("200 OK", "text/html", new Byte[0]);
        }

        private static Response MakeNotAllowedRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, (int)fileStream.Length);
            fileStream.Close();

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
            fileStream.Close();

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
            fileStream.Close();

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
