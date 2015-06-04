using System;
using System.Linq;
using Chess.Exceptions;

namespace Chess.UI.Console
{
    public class Match
    {
        private const char ArrowRight = (char)26;

        private readonly ScreenText _text;
        private readonly Screen _screen;

        private readonly ChessGame _game;

        public Match(ChessGame game)
        {
            _text = new ScreenText();
            _screen = new Screen();
            _game = game;
        }

        public void Start()
        {
            _game.Start();
            _screen.Print(_game);

            while (true)
            {
                string piece;
                var position = NextMove(out piece);

                try
                {
                    _game.Move(piece, position);
                    _screen.Print(_game);
                }
                catch (ChessException exception)
                {
                    _text.Error(exception.Message);
                }
            }
        }

        private string NextMove(out string piece)
        {
            System.Console.SetCursorPosition(0, 47);
            System.Console.WriteLine("");
            System.Console.WriteLine("           ");
            System.Console.WriteLine("");
            System.Console.WriteLine("                                                     ");
            System.Console.SetCursorPosition(0, 47);

            _text.NewLine();
            System.Console.WriteLine("   ╔═════════════╗");
            System.Console.WriteLine("   ║  next move  ║");
            System.Console.WriteLine("   ╚═════════════╝");
            _text.NewLine();
            _text.NewLine();

            _text.Write("   {0} piece ", ArrowRight);
            var file = GetFile();
            var rank = GetRank();

            piece = new string(new[] { file, rank });

            _text.Write(" move for ");

            file = GetFile();
            rank = GetRank();

            return new string(new[] { file, rank });
        }

        private char GetFile()
        {
            var files = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

            return GetKey(files.Contains, "( invalid file! please insert between a and h )");
        }

        private char GetRank()
        {
            var ranks = new[] { '8', '7', '6', '5', '4', '3', '2', '1' };

            return GetKey(ranks.Contains, "( invalid rank! please insert between 8 and 1 )");
        }

        private char GetKey(Func<char, bool> condition, string invalidMessage)
        {
            bool keyValid;
            char key;

            do
            {
                key = System.Console.ReadKey().KeyChar;
                keyValid = condition(key);

                if (!keyValid)
                {
                    _text.Error(invalidMessage);
                }
            } while (!keyValid);

            return key;
        }
    }
}