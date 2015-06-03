using System;

namespace Chess.UI.Console
{
    public class Screen
    {
        private readonly ScreenColor _color;
        private readonly ScreenText _text;

        public Screen()
        {
            _color = new ScreenColor();
            _text = new ScreenText();
        }

        public void Print(ChessGame game)
        {
            System.Console.Clear();

            System.Console.Write("CHESSBOARD");
            _text.NewLine();
            _text.NewLine();

            HeaderOrFooter(game);
            _text.Dash();

            var toggle = true;
            foreach (var rank in game.Ranks)
            {
                for (var i = 0; i < 3; i++)
                {
                    Rank(rank, toggle, game, !i.Equals(1));
                }

                _text.Dash();

                toggle = !toggle;
            }

            HeaderOrFooter(game);
        }

        private void Rank(char rank, bool toggle, ChessGame game, bool lacuna)
        {
            System.Console.Write("   {0} ", lacuna ? ' ' : rank);

            foreach (var file in game.Files)
            {
                _color.Toggle(toggle);

                if (lacuna)
                {
                    File(game);
                }
                else
                {
                    File(game, file, rank);
                }

                toggle = !toggle;

                _color.Restore();
            }

            _text.Pipe();
            System.Console.Write(" {0} ", lacuna ? ' ' : rank);
            _text.NewLine();
        }

        private void File(ChessGame game, char file = 'x', char rank = 'x')
        {
            _text.Pipe();

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

        private static void Piece(ChessPiece piece)
        {
            System.Console.ForegroundColor = piece.Player == 1 ? ConsoleColor.Red : ConsoleColor.Blue;

            System.Console.Write(" {0} ", piece.Name);

            System.Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Lacuna()
        {
            System.Console.Write("      ");
        }

        private void HeaderOrFooter(ChessGame game)
        {
            System.Console.Write("     ");

            foreach (var file in game.Files)
            {
                _text.Pipe();
                System.Console.Write("  {0}   ", file);
            }

            _text.Pipe();
            _text.NewLine();
        }
    }
}