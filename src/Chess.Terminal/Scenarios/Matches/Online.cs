namespace Chess.Terminal.Scenarios.Matches
{
    using Chess.Multiplayer;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Lib.Match;

    public class Online : Match<IGameMultiplayer>
    {
        private bool isServer;

        public Online(IGameMultiplayer game, IScreen screen, Chessboard chessboard)
            : base(game, screen, chessboard)
        {
            game.Played += this.OnPlayed;
        }

        protected Online()
        {
        }

        public void IsServer(bool value)
        {
            this.isServer = value;
        }

        protected override void Update()
        {
            if (this.isServer)
            {
                this.NextMove();
            }
            else
            {
                this.Game.WaitingTheMove();
            }
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            this.NextMove();
        }
    }
}