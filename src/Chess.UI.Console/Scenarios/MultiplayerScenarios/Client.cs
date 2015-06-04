using System;
using System.Net;

namespace Chess.UI.Console.Scenarios.MultiplayerScenarios
{
    public class Client : Scenario
    {
        public Client(ChessGame game)
            : base(game)
        {
            game.Error += OnError;
            game.Connected += OnConnected;
        }

        private void OnConnected()
        {
            var match = new Match(Game);
            match.Start();
        }

        private void OnError(Exception exception)
        {
            Text.Error(exception.Message);
            TryConnect();
        }

        protected override void Show()
        {
            Text.WriteInsideTheBox("connection data");
            Text.NewLine();
            Text.NewLine();

            TryConnect();
        }

        private void TryConnect()
        {
            Text.WriteWithSleep("   {0} ip address: ", ArrowRight);
            var ipAddress = System.Console.ReadLine();

            Text.WriteWithSleep("   {0} port: ", ArrowRight);
            var port = System.Console.ReadLine();

            Game.Connect(IPAddress.Parse(ipAddress), int.Parse(port));
        }
    }
}