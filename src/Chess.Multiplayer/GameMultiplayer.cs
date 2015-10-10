namespace Chess.Multiplayer
{
    using System.Net;

    using Chess.Multiplayer.EventHandlers;

    public class GameMultiplayer : Game, IGameMultiplayer
    {
        private readonly Server server;
        private readonly Client client;

        private Multiplayer player;

        public GameMultiplayer()
        {
            this.server = new Server();
            this.client = new Client();
        }

        public event ErrorEventHandler Error;

        public event PlayedEventHandler Played;

        public event ConnectedEventHandler Connected;

        public override void Move(string piecePosition, string newPosition)
        {
            base.Move(piecePosition, newPosition);

            if (this.player == null)
            {
                return;
            }

            this.player.SendTheMove(piecePosition, newPosition);
            this.player.WaitingTheMove();
        }

        public void WaitingTheMove()
        {
            this.player.WaitingTheMove();
        }

        public void WaitingForOpponent()
        {
            this.player = this.server;

            this.server.Connected += this.OnConnected;
            this.server.Played += this.OnPlayed;
            this.server.Error += this.Error;

            this.server.Listen();
        }

        public void Connect(string ipAddress, string port)
        {
            this.player = this.client;

            this.client.Connected += this.OnConnected;
            this.client.Played += this.OnPlayed;
            this.client.Error += this.Error;

            this.client.Connect(IPAddress.Parse(ipAddress), int.Parse(port));
        }

        private void OnConnected()
        {
            this.player.Played += this.OnPlayed;

            var handler = this.Connected;
            handler?.Invoke();
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            base.Move(piecePosition, newPosition);

            var handler = this.Played;
            handler?.Invoke(piecePosition, newPosition);
        }
    }
}