using Chess.UI.Console.Libs;

namespace Chess.UI.Console.Scenarios
{
    public class OpponentWaiting
    {
        private readonly Text _text;
        private readonly ChessGame _game;

        public OpponentWaiting(ChessGame game)
        {
            _text = new Text();

            _game = game;
            _game.Error += exception => _text.Error(exception.Message);
            _game.Waiting += () => _text.Info("waiting for a opponent");
            _game.DataReceived += message => _text.Info(message);
            _game.Connected += OnConnected;
        }

        private void OnConnected()
        {
            var match = new Match(_game);
            match.Start();
        }

        public void Start()
        {
            System.Console.Clear();

            _text.Title();

            System.Console.WriteLine("   ╔═════════════════════════╗");
            System.Console.WriteLine("   ║  waiting for oppoenent  ║");
            System.Console.WriteLine("   ╚═════════════════════════╝");
            _text.NewLine();
            _text.NewLine();

            _game.WaitingForOpponent();
        }
    }
}