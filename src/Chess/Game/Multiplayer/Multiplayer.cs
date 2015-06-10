using System;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;

namespace Chess.Game.Multiplayer
{
    internal class Multiplayer
    {
        protected Socket Socket;

        public Multiplayer(Socket socket)
        {
            Socket = socket;
        }

        public event PlayedEventHandler Played;
        public event ErrorEventHandler Error;

        public void SendTheMove(string piecePosition, string newPosition)
        {
            try
            {
                var move = string.Format("{0}->{1}", piecePosition, newPosition);
                var send = Encoding.ASCII.GetBytes(move);

                Socket.Send(send);
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        public void WaitingTheMove()
        {
            while (true)
            {
                var bytes = new byte[1024];
                var bytesRec = Socket.Receive(bytes);
                var move = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                if (string.IsNullOrEmpty(move))
                {
                    continue;
                }

                var args = move.Split(new[] { "->" }, StringSplitOptions.None);
                var piece = args[0];
                var newPosition = args[1];

                OnPlayed(piece, newPosition);
            }
        }

        public void Disconnect()
        {
            try
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
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
    }
}