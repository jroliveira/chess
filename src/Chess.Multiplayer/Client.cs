using System;
using System.Net;
using System.Net.Sockets;

namespace Chess.Multiplayer
{
    internal class Client : Multiplayer
    {
        public Client()
            : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        { }

        public void Connect(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                Client.Connect(endPoint);

                OnConnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }
    }
}
