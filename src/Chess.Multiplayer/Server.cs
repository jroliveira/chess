namespace Chess.Multiplayer
{
    using System;
    using System.Net;

    using Chess.Multiplayer.Socket;

    internal class Server : Multiplayer
    {
        private readonly ISocket server;

        public Server()
            : this(new TcpSocket())
        {
        }

        internal Server(ISocket server)
        {
            this.server = server;
        }

        public void Listen()
        {
            var ip = new IPAddress(0); // TODO: Get ip address

            try
            {
                this.server.Bind(ip, 11000);
                this.server.Listen();

                this.Socket = this.server.Accept();

                this.OnConnected();
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }
    }
}