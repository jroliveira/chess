namespace Chess.UI.Console.Scenarios.MultiplayerScenarios
{
    public class Waiting : Scenario
    {
        public Waiting(ChessGame game)
            : base(game)
        {
            game.Error += exception => Text.Error(exception.Message);
            game.Waiting += () => Text.Info("waiting for a opponent");
            game.DataReceived += message => Text.Info(message);
            game.Connected += OnConnected;
        }

        private void OnConnected()
        {
            var match = new Match(Game);
            match.Start();
        }

        protected override void Show()
        {
            Text.WriteInsideTheBox("waiting for oppoenent");
            Text.NewLine();
            Text.NewLine();

            Game.WaitingForOpponent();
        }
    }
}