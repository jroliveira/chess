namespace Chess.UI.Console.Scenarios.Matches
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Libs.Match;

    public class Online : Match
    {
        public Online(IGameMultiplayer game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, chessboard, writer, reader, screen)
        {
            game.Played += this.OnPlayed;
        }

        protected Online()
        {
        }

        public void Start(bool isServer)
        {
            this.Setup();

            this.Game.Start();
            this.Chessboard.Print(this.Game);

            while (true)
            {
                if (isServer)
                {
                    this.NextMove();
                }
                else
                {
                    this.Game.WaitingTheMove();
                }
            }
        }

        private void OnPlayed(string piecePosition, string newPosition)
        {
            this.Chessboard.Print(this.Game);
            this.NextMove();
        }
    }
}