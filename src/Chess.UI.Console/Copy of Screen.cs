using System;
using Chess.Extensions;

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
                    if (i.Equals(1))
                    {
                        Rank(rank, toggle, game);
                    }
                    else
                    {
                        System.Console.Write("     ");
                        for (var j = 0; j < 8; j++)
                        {
                            _color.Toggle(toggle);

                            _text.Pipe();
                            System.Console.Write("      ");

                            toggle = !toggle;
                            _color.Restore();
                        }
                        _text.Pipe();
                        System.Console.Write("   ");
                        _text.NewLine();
                    }
                }

                toggle = !toggle;
            }

            HeaderOrFooter(game);
        }

        private void Rank(char rank, bool toggle, ChessGame game)
        {
            System.Console.Write("   {0} ", rank);

            foreach (var file in game.Files)
            {
                _color.Toggle(toggle);

                File(file, rank, game);

                toggle = !toggle;

                _color.Restore();
            }

            _text.Pipe();
            System.Console.Write(" {0} ", rank);
            _text.NewLine();

            _text.Dash();
        }

        private void File(char file, char rank, ChessGame game)
        {
            var piece = game.GetPiece(file, rank);

            if (piece == null)
            {
                _text.Pipe();
                System.Console.Write("      ");
            }
            else
            {
                _text.Pipe();
                Piece(piece);
            }
        }

        private static void Piece(ChessPiece piece)
        {
            System.Console.ForegroundColor = piece.Player == 1 ? ConsoleColor.Red : ConsoleColor.Blue;

            System.Console.Write(" {0} ", piece.Name);

            System.Console.ForegroundColor = ConsoleColor.White;
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