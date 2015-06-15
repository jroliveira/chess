using System;
using System.Net;
using Chess.Multiplayer;
using Chess.UI.Console.Libs;
using Chess.UI.Console.Scenarios.Matches;

namespace Chess.UI.Console.Scenarios.Multiplayer
{
    public class Client : Scenario
    {
        private readonly Online _match;

        protected Client() { }

        public Client(IGameMultiplayer game, Online match, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            _match = match;
        }

        public virtual void Start()
        {
            Setup();

            Writer.WriteInsideTheBox("connection data");
            Writer.NewLine();
            Writer.NewLine();

            TryConnect();
        }

        private void TryConnect()
        {
            Writer.WriteWithSleep("   {0} ip address: ", ArrowRight);
            var ipAddress = Reader.ReadValue();

            Writer.WriteWithSleep("   {0} port: ", ArrowRight);
            var port = Reader.ReadValue();

            Game.Connected += OnConnected;
            Game.Error += OnError;
            Game.Connect(ipAddress, port);
        }

        private void OnConnected()
        {
            _match.Start(false);
        }

        private void OnError(Exception exception)
        {
            Writer.WriteError(exception.Message);
            TryConnect();
        }
    }
}