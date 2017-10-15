namespace Chess.Terminal.Lib.Match
{
    using System;

    using Chess.Lib;
    using Chess.Lib.Extensions;
    using Chess.Models;

    using static System.Console;

    public class Chessboard
    {
        private readonly Color color;
        private readonly Writer writer;
        private readonly Observer<IGame> observerGame;

        public Chessboard()
        {
            this.color = new Color();
            this.writer = new Writer();
            this.observerGame = new Observer<IGame>();
        }

        public void Subscribe(IGame game)
        {
            this.observerGame.Subscribe(game);
            this.observerGame.Updated += this.Update;
        }

        private static void Clear()
        {
            SetCursorPosition(0, 1);
        }

        private static void Piece(Piece piece)
        {
            Write(" {0} ", piece.Name);

            ForegroundColor = ConsoleColor.White;
        }

        private static void Lacuna()
        {
            Write("   ");
        }

        private void Update(IGame game)
        {
            Clear();
            this.HeaderOrFooter(game);
            this.writer.Divider(DividerPosition.Top);

            var toggle = true;
            game
                .Ranks
                .ForEach(rank =>
                {
                    for (var i = 0; i < 1; i++)
                    {
                        this.Rank(rank, toggle, game);
                    }

                    this.writer.Divider(game.Ranks.IsLast(rank) ? DividerPosition.Bottom : DividerPosition.Middle);

                    toggle = !toggle;
                });

            this.HeaderOrFooter(game);
        }

        private void Rank(char rank, bool toggle, IGame game)
        {
            Write("   {0} ", rank);

            game
                .Files
                .ForEach(file =>
                {
                    this.color.Toggle(toggle);

                    this.File(game, file, rank);

                    toggle = !toggle;

                    this.color.Restore();
                });

            this.writer.Pipe();
            Write(" {0} ", rank);
            this.writer.WriteNewLine();
        }

        private void File(IGame game, char file = 'x', char rank = 'x')
        {
            this.writer.Pipe();

            var piece = game.GetPiece(file, rank);

            if (piece == null)
            {
                Lacuna();
            }
            else
            {
                Piece(piece);
            }
        }

        private void HeaderOrFooter(IGame game)
        {
            Write("     ");

            game
                .Files
                .ForEach(file => Write("  {0} ", file));

            this.writer.WriteNewLine();
        }
    }
}