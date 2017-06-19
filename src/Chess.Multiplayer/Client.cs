namespace Chess.Multiplayer
{
    using System;
    using System.Net;

    using Chess.Multiplayer.Socket;

    internal class Client : Multiplayer
    {
        public Client()
            : base(new TcpSocket())
        {
        }

        internal Client(ISocket socket)
            : base(socket)
        {
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            try
            {
                this.Socket.Connect(ipAddress, port);

                this.OnConnected();
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }
    }
}
