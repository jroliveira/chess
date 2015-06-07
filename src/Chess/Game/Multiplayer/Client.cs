using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess.Game.Multiplayer
{
    public class Client
    {
        public void Connect(IPAddress ipAddress, int port)
        {
            var endPoint = new IPEndPoint(ipAddress, port);
            var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(endPoint);

                OnConnected();

                var message = Encoding.ASCII.GetBytes("This is a test");
                sender.Send(message);

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        public event ErrorEventHandler Error;

        protected virtual void OnError(Exception exception)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(exception);
            }
        }

        public event ConnectedEventHandler Connected;

        protected virtual void OnConnected()
        {
            var handler = Connected;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
