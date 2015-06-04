using System;

namespace Chess.UI.Console
{
    public class Main
    {
        private const char ArrowRight = (char)26;

        private readonly ScreenText _text;
        private readonly Multiplayer _multiplayer;
        private readonly Match _match;

        public Main()
        {
            System.Console.Title = "Chess";
            System.Console.SetWindowSize(84, 57);

            _text = new ScreenText();

            var game = new ChessGame();
            _multiplayer = new Multiplayer(game);
            _match = new Match(game);
        }

        public void Show()
        {
            System.Console.Clear();

            System.Console.Write(@"
           _                        
          | |                       
     ___  | |__     ___   ___   ___ 
    / __| | '_ \   / _ \ / __| / __|
   | (__  | | | | |  __/ \__ \ \__ \
    \___| |_| |_|  \___| |___/ |___/
  
            ");

            _text.NewLine();
            _text.NewLine();

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