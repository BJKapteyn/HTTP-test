using System;
using System.Collections.Generic;
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
                TcpClient client = new TcpClient();

                HandleClient(client);

                client.Close();
            }

            Running = false;

            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {

        }
    }
}
