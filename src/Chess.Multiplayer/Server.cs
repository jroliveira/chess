using System;
using System.Net;
using Chess.Multiplayer.Socket;

namespace Chess.Multiplayer
{
    internal class Server : Multiplayer
    {
        private readonly ISocket _server;

        internal Server(ISocket server)
        {
            _server = server;
        }

        public Server()
            : this(new TcpSocket())
        {

        }

        public void Listen()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ip = host.AddressList[1];

            try
            {
                _server.Bind(ip, 11000);
                _server.Listen();

                Socket = _server.Accept();

                OnConnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }
    }
}