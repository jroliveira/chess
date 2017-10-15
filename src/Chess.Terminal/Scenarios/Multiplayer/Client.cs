namespace Chess.Terminal.Scenarios.Multiplayer
{
    using System;

    using Chess.Multiplayer;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Scenarios.Matches;

    public class Client : Scenario<IGameMultiplayer>
    {
        private readonly Online match;

        public Client(IGameMultiplayer game, IScreen screen, Online match)
            : base(game, screen)
        {
            this.match = match;
        }

        protected Client()
        {
        }

        protected override void Initialize()
        {
            this.SetTitle("connection data");
            this.TryConnect();
        }

        private void TryConnect()
        {
            var ipAddress = this.RequestOption("   ip address: ", this.Screen.GetString);
            var port = this.RequestOption("   port: ", this.Screen.GetString);

            this.Game.Connected += this.OnConnected;
            this.Game.Error += this.OnError;
            this.Game.Connect(ipAddress, port);
        }

        private void OnConnected()
        {
            this.match.IsServer(false);
            this.match.Start();
        }

        private void OnError(Exception exception)
        {
            this.Screen.WriteError(exception.Message);
            this.TryConnect();
        }
    }
}