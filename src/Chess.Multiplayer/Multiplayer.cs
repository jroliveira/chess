namespace Chess.Multiplayer
{
    using System;

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

        public event Action<Exception> Error;

        public event Action Connected;

        public event Action<string, string> Played;

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

        private void OnPlayed(string piecePosition, string newPosition)
        {
            var handler = this.Played;
            handler?.Invoke(piecePosition, newPosition);
        }
    }
}