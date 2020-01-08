using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPTest
{
    public class HTTPServer
    {
        public const string MSG_DIR = "/root/msg/";
        public const string WEB_DIR = "/root/web/";
        public const string Name = "Brad's rad server";
        public const string Version = "HTML/1.1";

        private bool Running = false;

        private TcpListener listener;

        public HTTPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);

        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            Running = true;
            listener.Start();

            while(Running)
            {
                Console.WriteLine("Waiting for connection");

                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Client connected!");

                HandleClient(client);

                client.Close();
            }

            Running = false;

            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());

            string msg = "";

            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + " ";
            }

            Debug.WriteLine("Request: \n" + msg);

            Request request = Request.GetRequest(msg);

            Response response = Response.From(request);
            response.Post(client.GetStream());

        }
    }
}
