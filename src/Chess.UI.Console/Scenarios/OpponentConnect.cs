using System;
using System.Net;
using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class OpponentConnect
    {
        private const char ArrowRight = (char)26;

        private readonly Text _text;
        private readonly ChessGame _game;

        public OpponentConnect(ChessGame game)
        {
            _text = new Text();

            _game = game;
            _game.Error += OnError;
            _game.Connected += OnConnected;
        }

        private void OnConnected()
        {
            var match = new Match(_game);
            match.Start();
        }

        private void OnError(Exception exception)
        {
            _text.Error(exception.Message);
            Connect();
        }

        public void Start()
        {
            System.Console.Clear();
            _text.Title();

            System.Console.WriteLine("   ╔═══════════════════╗");
            System.Console.WriteLine("   ║  connection data  ║");
            System.Console.WriteLine("   ╚═══════════════════╝");
            _text.NewLine();
            _text.NewLine();

            Connect();
        }

        private void Connect()
        {
            _text.Write("   {0} ip address: ", ArrowRight);
            var ipAddress = System.Console.ReadLine();

            _text.Write("   {0} port: ", ArrowRight);
            var port = System.Console.ReadLine();

            _game.Connect(IPAddress.Parse(ipAddress), int.Parse(port));
        }
    }
}