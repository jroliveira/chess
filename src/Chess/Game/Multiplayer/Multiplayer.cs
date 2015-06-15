using System;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess.Game.Multiplayer
{
    internal class Multiplayer
    {
        protected Socket Client;

        public Multiplayer(Socket socket)
        {
            Client = socket;
        }

        public event PlayedEventHandler Played;
        public event ErrorEventHandler Error;
        public event ConnectedEventHandler Connected;
        public event DisconnectedEventHandler Disconnected;

        public void SendTheMove(string piecePosition, string newPosition)
        {
            try
            {
                var move = string.Format("{0}->{1}", piecePosition, newPosition);
                var send = Encoding.ASCII.GetBytes(move);

                Client.Send(send);
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        public void WaitingTheMove()
        {
            var bytes = new byte[1024];

            try
            {
                var bytesRec = Client.Receive(bytes);
                var move = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                var args = move.Split(new[] { "->" }, StringSplitOptions.None);
                var piece = args[0];
                var newPosition = args[1];

                OnPlayed(piece, newPosition);
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        public void Disconnect()
        {
            try
            {
                Client.Shutdown(SocketShutdown.Both);
                Client.Close();

                OnDisconnected();
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            var handler = Played;
            if (handler != null)
            {
                handler(piecePosition, newPosition);
            }
        }

        protected void OnError(Exception exception)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(exception);
            }
        }

        protected void OnConnected()
        {
            var handler = Connected;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnDisconnected()
        {
            var handler = Disconnected;
            if (handler != null)
            {
                handler();
            }
        }
    }
}