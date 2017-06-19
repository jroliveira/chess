namespace Chess.Multiplayer
{
    using System;

    using Chess.Multiplayer.EventHandlers;
    using Chess.Multiplayer.Socket;

    internal class Multiplayer
    {
        public Multiplayer(ISocket socket)
        {
            this.Socket = socket;
        }

        protected Multiplayer()
        {
        }

        public event PlayedEventHandler Played;

        public event ErrorEventHandler Error;

        public event ConnectedEventHandler Connected;

        public event DisconnectedEventHandler Disconnected;

        protected ISocket Socket { get; set; }

        public void SendTheMove(string piecePosition, string newPosition)
        {
            try
            {
                var move = $"{piecePosition}->{newPosition}";

                this.Socket.Send(move);
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }

        public void WaitingTheMove()
        {
            try
            {
                var move = this.Socket.Receive();

                var args = move.Split(new[] { "->" }, StringSplitOptions.None);
                var piece = args[0];
                var newPosition = args[1];

                this.OnPlayed(piece, newPosition);
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }

        public void Disconnect()
        {
            try
            {
                this.Socket.Shutdown();
                this.Socket.Close();

                this.OnDisconnected();
            }
            catch (Exception exception)
            {
                this.OnError(exception);
            }
        }

        protected void OnError(Exception exception)
        {
            var handler = this.Error;
            handler?.Invoke(exception);
        }

        protected void OnConnected()
        {
            var handler = this.Connected;
            handler?.Invoke();
        }

        protected virtual void OnDisconnected()
        {
            var handler = this.Disconnected;
            handler?.Invoke();
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            var handler = this.Played;
            handler?.Invoke(piecePosition, newPosition);
        }
    }
}