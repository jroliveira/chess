using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        public event PlayedEventHandler Played;

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

                while (true)
                {
                    var bytes = new byte[1024];
                    var bytesRec = Socket.Receive(bytes);
                    var move = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    if (string.IsNullOrEmpty(move))
                    {
                        continue;
                    }

                    var args = move.Split(new[] { "->" }, StringSplitOptions.None);
                    var piece = args[0];
                    var newPosition = args[1];

                    OnPlayed(piece, newPosition);
                }
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

        private void OnPlayed(string piece, string newPosition)
        {
            var handler = Played;
            if (handler != null)
            {
                handler(piece, newPosition);
            }
        }
    }
}