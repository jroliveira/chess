namespace Chess.UI.Console.Libs.Match
{
    using System;

    using Chess.Extensions;
    using Chess.Models;

    using static System.Console;

    public class Chessboard
    {
        private readonly Color color;
        private readonly Writer text;

        public Chessboard()
        {
            this.color = new Color();
            this.text = new Writer();
        }

        public virtual void Print(IGame game)
        {
            Clear();
            this.HeaderOrFooter(game);
            this.text.Divider(DividerPosition.Top);

            var toggle = true;
            foreach (var rank in game.Ranks)
            {
                for (var i = 0; i < 1; i++)
                {
                    this.Rank(rank, toggle, game);
                }

                if (game.Ranks.IsLast(rank))
                {
                    this.text.Divider(DividerPosition.Bottom);
                }
                else
                {
                    this.text.Divider(DividerPosition.Middle);
                }

                toggle = !toggle;
            }

            this.HeaderOrFooter(game);
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

        private void Rank(char rank, bool toggle, IGame game)
        {
            Write("   {0} ", rank);

            foreach (var file in game.Files)
            {
                this.color.Toggle(toggle);

                this.File(game, file, rank);

                toggle = !toggle;

                this.color.Restore();
            }

            this.text.Pipe();
            Write(" {0} ", rank);
            this.text.NewLine();
        }

        private void File(IGame game, char file = 'x', char rank = 'x')
        {
            this.text.Pipe();

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

            foreach (var file in game.Files)
            {
                Write("  {0} ", file);
            }

            this.text.NewLine();
        }
    }
}