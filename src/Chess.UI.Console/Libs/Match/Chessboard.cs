using System;
using Chess.Extensions;
using Chess.Models;

namespace Chess.UI.Console.Libs.Match
{
    public class Chessboard
    {
        private readonly Color _color;
        private readonly Writer _text;

        public Chessboard()
        {
            _color = new Color();
            _text = new Writer();
        }

        public virtual void Print(IGame game)
        {
            Clear();
            HeaderOrFooter(game);
            _text.Divider(DividerPosition.Top);

            var toggle = true;
            foreach (var rank in game.Ranks)
            {
                for (var i = 0; i < 1; i++)
                {
                    Rank(rank, toggle, game);
                }

                if (game.Ranks.IsLast(rank))
                {
                    _text.Divider(DividerPosition.Bottom);
                }
                else
                {
                    _text.Divider(DividerPosition.Middle);
                }

                toggle = !toggle;
            }

            HeaderOrFooter(game);
        }

        private static void Clear()
        {
            System.Console.SetCursorPosition(0, 1);
        }

        private void Rank(char rank, bool toggle, IGame game)
        {
            System.Console.Write("   {0} ", rank);

            foreach (var file in game.Files)
            {
                _color.Toggle(toggle);

                File(game, file, rank);

                toggle = !toggle;

                _color.Restore();
            }

            _text.Pipe();
            System.Console.Write(" {0} ", rank);
            _text.NewLine();
        }

        private void File(IGame game, char file = 'x', char rank = 'x')
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

        private static void Piece(Piece piece)
        {
            System.Console.Write(" {0} ", piece.Name);

            System.Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Lacuna()
        {
            System.Console.Write("   ");
        }

        private void HeaderOrFooter(IGame game)
        {
            System.Console.Write("     ");

            foreach (var file in game.Files)
            {
                System.Console.Write("  {0} ", file);
            }

            _text.NewLine();
        }
    }
}