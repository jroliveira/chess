using System;
using System.Net;
using Chess.Multiplayer.Socket;

namespace Chess.Multiplayer
{
    internal class Client : Multiplayer
    {
        internal Client(ISocket socket)
            : base(socket)
        {

        }

        public Client()
            : base(new TcpSocket())
        {

        }

        public void Connect(IPAddress ipAddress, int port)
        {
            try
            {
                Socket.Connect(ipAddress, port);

                OnConnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }
    }
}
