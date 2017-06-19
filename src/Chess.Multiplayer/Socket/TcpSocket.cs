namespace Chess.Multiplayer.Socket
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    using SystemSocket = System.Net.Sockets.Socket;

    internal class TcpSocket : ISocket
    {
        private readonly SystemSocket socket;

        public TcpSocket(SystemSocket socket)
        {
            this.socket = socket;
        }

        public TcpSocket()
            : this(new SystemSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
        }

        public void Connect(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            this.socket.Connect(endPoint);
        }

        public int Send(string message)
        {
            var buffer = Encoding.ASCII.GetBytes(message);

            return this.socket.Send(buffer);
        }

        public string Receive()
        {
            var buffer = new byte[1024];

            var bytesRec = this.socket.Receive(buffer);

            return Encoding.ASCII.GetString(buffer, 0, bytesRec);
        }

        public void Shutdown()
        {
            this.socket.Shutdown(SocketShutdown.Both);
        }

        public void Close()
        {
            this.socket.Close();
        }

        public ISocket Accept()
        {
            var client = this.socket.Accept();

            return new TcpSocket(client);
        }

        public void Listen()
        {
            this.socket.Listen(1);
        }

        public void Bind(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            this.socket.Bind(endPoint);
        }
    }
}
