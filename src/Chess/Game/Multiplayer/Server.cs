using System;
using System.Net;
using System.Net.Sockets;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess.Game.Multiplayer
{
    internal class Server : Multiplayer
    {
        private readonly Socket _listener;

        public Server()
            : base(null)
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public event ConnectedEventHandler Connected;

        public void Listening()
        {
            var host = Dns.Resolve(Dns.GetHostName());
            var ip = host.AddressList[0];
            var endPoint = new IPEndPoint(ip, 11000);

            try
            {
                _listener.Bind(endPoint);
                _listener.Listen(1);

                Socket = _listener.Accept();

                OnConnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        private void OnConnected()
        {
            var handler = Connected;
            if (handler != null)
            {
                handler();
            }
        }
    }
}