namespace Chess.UI.Console.Scenarios.Matches
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Libs.Match;

    public class Offline : Match
    {
        public Offline(IGameMultiplayer game, Chessboard chessboard, IWriter writer, IReader reader, IScreen screen)
            : base(game, chessboard, writer, reader, screen)
        {
        }

        protected Offline()
        {
        }

        public virtual void Start()
        {
            this.Setup();

            this.Game.Start();
            this.Chessboard.Print(this.Game);

            while (true)
            {
                this.NextMove();
            }
        }
    }
}