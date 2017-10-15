namespace Chess.Terminal.Scenarios.Matches
{
    using Chess.Terminal.Lib;
    using Chess.Terminal.Lib.Match;

    public class Offline : Match<IGame>
    {
        public Offline(IGame game, IScreen screen, Chessboard chessboard)
            : base(game, screen, chessboard)
        {
        }

        protected Offline()
        {
        }

        protected override void Update()
        {
            this.NextMove();
        }
    }
}