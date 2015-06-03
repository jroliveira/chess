using System;

namespace Chess.UI.Console.Game
{
    public class Game
    {
        private readonly Chessboard _chessboard;
        private readonly Print _print;

        public Game()
        {
            _print = new Print();
            _chessboard = new Chessboard();
        }

        public void Start()
        {
            _chessboard.Start();
            _print.Execute(_chessboard);
        }

        public void Move(string piecePosition, string newPosition)
        {
            var piece = _chessboard.GetPiece(piecePosition);
            _chessboard.MovePiece(piece, newPosition);
            _print.Execute(_chessboard);
        }
    }

    public class Print
    {
        public void Execute(Chessboard chessboard)
        {
            System.Console.Clear();

            Title();

            HeaderOrFooter(chessboard);
            Dash();

            var toggle = true;
            foreach (var rank in chessboard.Ranks)
            {
                Rank(rank, chessboard, toggle);

                toggle = !toggle;
            }

            HeaderOrFooter(chessboard);
        }

        private static void Title()
        {
            System.Console.WriteLine("CHESSBOARD");
            System.Console.WriteLine("");
        }

        private static void HeaderOrFooter(Chessboard chessboard)
        {
            System.Console.Write("     ");
            foreach (var file in chessboard.Files)
            {
                System.Console.Write("|  {0}   ", file);
            }
            System.Console.WriteLine("|");
        }

        private static void Rank(char rank, Chessboard chessboard, bool toggle)
        {
            System.Console.Write("   {0} ", rank);

            foreach (var file in chessboard.Files)
            {
                if (toggle)
                {
                    System.Console.BackgroundColor = ConsoleColor.White;
                    System.Console.ForegroundColor = ConsoleColor.Black;
                }

                File(file, rank, chessboard);

                System.Console.BackgroundColor = ConsoleColor.Black;
                System.Console.ForegroundColor = ConsoleColor.White;

                toggle = !toggle;
            }

            System.Console.WriteLine("| {0} ", rank);
            Dash();
        }

        private static void File(char file, char rank, Chessboard chessboard)
        {
            var position = new Position(file, rank);
            var piece = chessboard.GetPiece(position.ToString());

            if (piece == null)
            {
                Pipe();

                System.Console.Write("      ");
            }
            else
            {
                Pipe();

                var name = piece.GetType().Name.Substring(0, 4);
                System.Console.Write(" {0} ", name);
            }
        }

        private static void Dash()
        {
            System.Console.WriteLine("  ---------------------------------------------------------------");
        }

        private static void Pipe()
        {
            var current = System.Console.BackgroundColor;

            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;

            System.Console.Write("|");

            if (current != ConsoleColor.Black)
            {
                System.Console.BackgroundColor = ConsoleColor.White;
                System.Console.ForegroundColor = ConsoleColor.Black;
            }
        }
    }
}