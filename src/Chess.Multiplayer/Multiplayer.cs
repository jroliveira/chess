using System;
using Chess.Multiplayer.EventHandlers;
using Chess.Multiplayer.Socket;

namespace Chess.Multiplayer
{
    internal class Multiplayer
    {
        protected ISocket Socket;

        protected Multiplayer()
        {

        }

        public Multiplayer(ISocket socket)
        {
            Socket = socket;
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

                Socket.Send(move);
            }
            catch (Exception exception)
            {
                OnError(exception);
            }
        }

        public void WaitingTheMove()
        {
            try
            {
                var move = Socket.Receive();

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
                Socket.Shutdown();
                Socket.Close();

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