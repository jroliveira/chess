namespace Chess.Terminal.Scenarios.Matches
{
    using System.Linq;

    using Chess.Lib;
    using Chess.Lib.Exceptions;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Lib.Match;

    public abstract class Match<TGame> : Scenario<TGame>
        where TGame : IGame
    {
        protected readonly Chessboard Chessboard;

        protected Match(TGame game, IScreen screen, Chessboard chessboard)
            : base(game, screen)
        {
            var observerGame = new Observer<IGame>();
            observerGame.Subscribe(game);

            this.Chessboard = chessboard;
            this.Chessboard.Subscribe(this.Game);
        }

        protected Match()
        {
        }

        protected override void Initialize()
        {
            this.Game.Start();

            while (true)
            {
                this.Update();
            }
        }

        protected abstract void Update();

        protected void NextMove()
        {
            this.Screen.ClearOption();

            var (file, rank) = this.RequestOption("   NEXT MOVE -> piece ", this.ReadOption);
            var piecePosition = new string(new[] { file, rank });

            var (newFile, newRank) = this.RequestOption(" move for ", this.ReadOption);
            var newPosition = new string(new[] { newFile, newRank });

            try
            {
                this.Game.Move(piecePosition, newPosition);
                this.Screen.ClearOption();
            }
            catch (ChessException exception)
            {
                this.Screen.WriteError(exception.Message);
            }
        }

        private (char, char) ReadOption()
        {
            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            var ranks = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            var file = this.ReadOption(files.Contains, "Insert between a and h")();
            var rank = this.ReadOption(ranks.Contains, "Insert between 8 and 1")();

            return (file, rank);
        }
    }
}
