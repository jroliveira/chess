namespace Chess.UI.Console.Scenarios.Multiplayer
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Scenarios.Matches;

    public class Server : Scenario
    {
        private readonly Online match;

        public Server(IGameMultiplayer game, Online match, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            this.match = match;
        }

        protected Server()
        {
        }

        public virtual void Start()
        {
            this.Setup();

            this.Writer.WriteInsideTheBox("waiting for oppoenent");
            this.Writer.NewLine();
            this.Writer.NewLine();

            this.Game.Connected += this.OnConnected;
            this.Game.Error += exception => this.Writer.WriteError(exception.Message);
            this.Game.WaitingForOpponent();
        }

        private void OnConnected()
        {
            this.match.Start(true);
        }
    }
}