namespace Chess.Terminal.Scenarios.Multiplayer
{
    using Chess.Multiplayer;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Scenarios.Matches;

    public class Server : Scenario<IGameMultiplayer>
    {
        private readonly Online match;

        public Server(IGameMultiplayer game, IScreen screen, Online match)
            : base(game, screen)
        {
            this.match = match;
        }

        protected Server()
        {
        }

        protected override void Initialize()
        {
            this.SetTitle("waiting for oppoenent");

            this.Game.Connected += this.OnConnected;
            this.Game.Error += exception => this.Screen.WriteError(exception.Message);
            this.Game.WaitingForOpponent();
        }

        private void OnConnected()
        {
            this.match.IsServer(true);
            this.match.Start();
        }
    }
}