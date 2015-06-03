using System;
using System.Linq;
using Chess.Exceptions;

namespace Chess.UI.Console
{
    class Program
    {
        private const char ArrowRight = (char)26;

        private static readonly ScreenText Text = new ScreenText();
        private static readonly Screen Screen = new Screen();

        static void Main()
        {
            System.Console.Title = "Chess";
            System.Console.SetWindowSize(84, 42);

            var game = new ChessGame();
            game.Start();

            Screen.Print(game);

            while (true)
            {
                string piece;
                var position = NextMove(out piece);

                try
                {
                    game.Move(piece, position);
                    Screen.Print(game);
                }
                catch (ChessException exception)
                {
                    Text.Error(exception.Message);
                }
            }
        }

        static string NextMove(out string piece)
        {
            System.Console.SetCursorPosition(0, 37);
            System.Console.WriteLine("");
            System.Console.WriteLine("           ");
            System.Console.WriteLine("");
            System.Console.WriteLine("                                                     ");
            System.Console.SetCursorPosition(0, 37);

            Text.NewLine();
            Text.Write("NEXT MOVE {0}", (char)1);
            Text.NewLine();
            Text.NewLine();

            Text.Write(" {0} Piece ", ArrowRight);
            var file = GetFile();
            var rank = GetRank();

            piece = new string(new[] { file, rank });

            Text.Write(" move for ");

            file = GetFile();
            rank = GetRank();

            return new string(new[] { file, rank });
        }

        static char GetFile()
        {
            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            return GetKey(files.Contains, "( invalid file! please insert between a and h )");
        }

        static char GetRank()
        {
            var ranks = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            return GetKey(ranks.Contains, "( invalid rank! please insert between 8 and 1 )");
        }

        static char GetKey(Func<char, bool> condition, string invalidMessage)
        {
            bool keyValid;
            char key;

            do
            {
                key = System.Console.ReadKey().KeyChar;
                keyValid = condition(key);

                if (!keyValid)
                {
                    Text.Error(invalidMessage);
                }
            } while (!keyValid);

            return key;
        }
    }
}
