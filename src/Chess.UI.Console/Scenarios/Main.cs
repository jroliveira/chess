using System;
using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class Main
    {
        private const char ArrowRight = (char)26;

        private readonly Text _text;
        private readonly Multiplayer _multiplayer;
        private readonly Match _match;

        public Main()
        {
            System.Console.Title = "Chess";
            System.Console.SetWindowSize(84, 57);

            _text = new Text();

            var game = new ChessGame();
            _multiplayer = new Multiplayer(game);
            _match = new Match(game);
        }

        public void Start()
        {
            System.Console.Clear();

            _text.Title();

            System.Console.WriteLine("   ╔════════════════════╗");
            System.Console.WriteLine("   ║  choose an option  ║");
            System.Console.WriteLine("   ╚════════════════════╝");
            _text.NewLine();
            _text.NewLine();
            System.Console.WriteLine("   ╔═══╗");
            System.Console.WriteLine("   ║ 1 ║ solo");
            System.Console.WriteLine("   ╚═══╝");
            _text.NewLine();
            System.Console.WriteLine("   ╔═══╗");
            System.Console.WriteLine("   ║ 2 ║ multiplayer");
            System.Console.WriteLine("   ╚═══╝");
            _text.NewLine();
            _text.NewLine();
            _text.Write("   {0} option: ", ArrowRight);

            var option = GetKey(key => key.Equals('1') || key.Equals('2'), "invalid option! please insert 1 or 2");

            if (option.Equals('1'))
            {
                _match.Start();
            }
            else
            {
                _multiplayer.Start();
            }
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