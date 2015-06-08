using System;
using System.Net.Sockets;
using System.Text;
using Chess.Game.Multiplayer.EventHandlers;
using Chess.Game.Pieces;

namespace Chess.Game.Multiplayer
{
    internal class Multiplayer
    {
        protected Socket Socket;

        public Multiplayer(Socket socket)
        {
            Socket = socket;
        }

        public event ErrorEventHandler Error;

        public void SendsTheMove(Piece piece, Position newPosition)
        {
            try
            {
                var move = string.Format("{0}->{1}", piece.Position, newPosition);
                var send = Encoding.ASCII.GetBytes(move);

                Socket.Send(send);
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
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
            catch (Exception exception)
            {
                OnError(exception);
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