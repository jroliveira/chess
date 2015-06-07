using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess.Game.Multiplayer
{
    public class Server
    {
        public void Listening()
        {
            var host = Dns.Resolve(Dns.GetHostName());
            var ip = host.AddressList[0];
            var endPoint = new IPEndPoint(ip, 11000);

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(endPoint);
                listener.Listen(1);

                while (true)
                {
                    OnWaiting();

                    var handler = listener.Accept();

                    OnConnected();

                    while (true)
                    {
                        var bytes = new byte[1024];
                        var bytesRec = handler.Receive(bytes);
                        var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (string.IsNullOrEmpty(data))
                        {
                            continue;
                        }

                        OnDataReceived(data);

                        if (data.Equals("/end"))
                        {
                            break;
                        }
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
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

        public event WaitingEventHandler Waiting;

        protected virtual void OnWaiting()
        {
            var handler = Waiting;
            if (handler != null)
            {
                handler();
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

        public event DataReceivedEventHandler DataReceived;

        protected virtual void OnDataReceived(string data)
        {
            var handler = DataReceived;
            if (handler != null)
            {
                handler(data);
            }
        }
    }
}