using Chess.UI.Console.Libs;
using Chess.UI.Console.Scenarios.Matches;

namespace Chess.UI.Console.Scenarios.Multiplayer
{
    public class Server : Scenario
    {
        private readonly Online _match;

        protected Server() { }

        public Server(ChessGame game, Online match, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            _match = match;
        }

        public virtual void Start()
        {
            Setup();

            Writer.WriteInsideTheBox("waiting for oppoenent");
            Writer.NewLine();
            Writer.NewLine();

            Game.Connected += OnConnected;
            Game.Error += exception => Writer.WriteError(exception.Message);
            Game.WaitingForOpponent();
        }

        private void OnConnected()
        {
            _match.Start(true);
        }
    }
}