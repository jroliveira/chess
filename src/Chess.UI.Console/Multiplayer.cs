using System;

namespace Chess.UI.Console
{
    public class Multiplayer
    {
        private const char ArrowRight = (char)26;

        private readonly ScreenText _text;
        private readonly ChessGame _game;

        public Multiplayer(ChessGame game)
        {
            _text = new ScreenText();
            _game = game;
        }

        public void Start()
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
                _game.Error += exception => _text.Error(exception.Message);
                _game.Waiting += () => _text.Info("waiting for a opponent");
                _game.DataReceived += message => _text.Info(message);

                _game.WaitingForOpponent();
            }
            else
            {
                _game.Error += exception => _text.Error(exception.Message);
                _game.Connected += () => _text.Info("connected");

                _game.Connect();
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