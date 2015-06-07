using System;
using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class Multiplayer
    {
        private const char ArrowRight = (char)26;

        private readonly Text _text;
        private readonly ChessGame _game;

        public Multiplayer(ChessGame game)
        {
            _text = new Text();
            _game = game;
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
            System.Console.WriteLine("   ║ 1 ║ waiting for opponent");
            System.Console.WriteLine("   ╚═══╝");
            _text.NewLine();
            System.Console.WriteLine("   ╔═══╗");
            System.Console.WriteLine("   ║ 2 ║ connect an one opponent");
            System.Console.WriteLine("   ╚═══╝");
            _text.NewLine();
            _text.NewLine();
            _text.Write("   {0} option: ", ArrowRight);

            var option = GetKey(key => key.Equals('1') || key.Equals('2'), "invalid option! please insert 1 or 2");

            if (option.Equals('1'))
            {
                var opponentWaiting = new OpponentWaiting(_game);
                opponentWaiting.Start();
            }
            else
            {
                var opponentConnect = new OpponentConnect(_game);
                opponentConnect.Start();
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