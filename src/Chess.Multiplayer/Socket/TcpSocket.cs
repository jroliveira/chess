using System.Net;
using System.Net.Sockets;
using System.Text;
using SystemSocket = System.Net.Sockets.Socket;

namespace Chess.Multiplayer.Socket
{
    internal class TcpSocket : ISocket
    {
        private readonly SystemSocket _socket;

        public TcpSocket(SystemSocket socket)
        {
            _socket = socket;
        }

        public TcpSocket()
            : this(new SystemSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {

        }

        public void Connect(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            _socket.Connect(endPoint);
        }

        public int Send(string message)
        {
            var buffer = Encoding.ASCII.GetBytes(message);

            return _socket.Send(buffer);
        }

        public string Receive()
        {
            var buffer = new byte[1024];

            var bytesRec = _socket.Receive(buffer);

            return Encoding.ASCII.GetString(buffer, 0, bytesRec);
        }

        public void Shutdown()
        {
            _socket.Shutdown(SocketShutdown.Both);
        }

        public void Close()
        {
            _socket.Close();
        }

        public ISocket Accept()
        {
            var socket = _socket.Accept();

            return new TcpSocket(socket);
        }

        public void Listen()
        {
            _socket.Listen(1);
        }

        public void Bind(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            _socket.Bind(endPoint);
        }
    }
}
