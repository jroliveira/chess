using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;
using Chess.Game.Pieces;

namespace Chess.Game.Multiplayer
{
    internal class Client : Multiplayer
    {
        public Client()
            : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        { }

        public event ConnectedEventHandler Connected;

        public void Connect(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Socket.Connect(endPoint);

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
