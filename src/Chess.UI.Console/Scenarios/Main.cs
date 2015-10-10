namespace Chess.UI.Console.Scenarios
{
    using System;
    using System.Collections.Generic;

    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Scenarios.Matches;
    using Chess.UI.Console.Scenarios.Multiplayer;

    public class Main : Scenario
    {
        private readonly IDictionary<char, Action> options;

        public Main(IGameMultiplayer game, Offline match, Client client, Server server, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            screen.Title = "Chess";
            screen.Size = new Size { Height = 25, Width = 48 };

            this.options = new Dictionary<char, Action>
            {
                { '1', match.Start },
                { '2', server.Start },
                { '3', client.Start }
            };
        }

        public void Start()
        {
            this.Setup();

            this.Writer.WriteInsideTheBox("choose an option");
            this.Writer.NewLine();
            this.Writer.NewLine();

            this.Writer.WriteOption("1", "solo");
            this.Writer.NewLine();
            this.Writer.WriteOption("2", "waiting for opponent");
            this.Writer.NewLine();
            this.Writer.WriteOption("3", "connect an one opponent");
            this.Writer.NewLine();
            this.Writer.WriteWithSleep("   option: ");

            var option = this.ReadOption();
            this.options[option]();
        }

        private char ReadOption()
        {
            bool isValid;
            char option;

            do
            {
                option = this.Reader.ReadKey();
                isValid = this.options.ContainsKey(option);

                if (!isValid)
                {
                    this.Writer.WriteError("invalid option! please insert [1], [2] or [3]");
                }
            }
            while (!isValid);

            return option;
        }
    }
}