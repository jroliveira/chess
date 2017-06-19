namespace Chess.UI.Console.Scenarios.Multiplayer
{
    using System;

    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Scenarios.Matches;

    public class Client : Scenario
    {
        private readonly Online match;

        public Client(IGameMultiplayer game, Online match, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            this.match = match;
        }

        protected Client()
        {
        }

        public virtual void Start()
        {
            this.Setup();

            this.Writer.WriteInsideTheBox("connection data");
            this.Writer.NewLine();
            this.Writer.NewLine();

            this.TryConnect();
        }

        private void TryConnect()
        {
            this.Writer.WriteWithSleep("   ip address: ");
            var ipAddress = this.Reader.ReadValue();

            this.Writer.WriteWithSleep("   port: ");
            var port = this.Reader.ReadValue();

            this.Game.Connected += this.OnConnected;
            this.Game.Error += this.OnError;
            this.Game.Connect(ipAddress, port);
        }

        private void OnConnected()
        {
            this.match.Start(false);
        }

        private void OnError(Exception exception)
        {
            this.Writer.WriteError(exception.Message);
            this.TryConnect();
        }
    }
}