using System;
using System.Net;
using System.Net.Sockets;

namespace Chess.Multiplayer
{
    internal class Server : Multiplayer
    {
        private readonly Socket _server;

        public Server()
            : base(null)
        {
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Listen()
        {
            var host = Dns.Resolve(Dns.GetHostName());
            var ip = host.AddressList[0];
            var endPoint = new IPEndPoint(ip, 11000);

            try
            {
                _server.Bind(endPoint);
                _server.Listen(1);

                Client = _server.Accept();

                OnConnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }
    }
}