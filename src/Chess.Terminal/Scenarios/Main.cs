namespace Chess.Terminal.Scenarios
{
    using System;
    using System.Collections.Generic;

    using Chess.Lib.Extensions;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Scenarios.Matches;
    using Chess.Terminal.Scenarios.Multiplayer;

    public class Main : Scenario<IGame>
    {
        private readonly IDictionary<char, (string Caption, Action Start)> options;

        public Main(IGame game, IScreen screen, Offline match, Client client, Server server)
            : base(game, screen)
        {
            this.options = new Dictionary<char, (string Label, Action)>
            {
                { '1', ("solo", match.Start) },
                { '2', ("waiting for opponent", server.Start) },
                { '3', ("connect an one opponent", client.Start) }
            };
        }

        protected override void Initialize()
        {
            this.SetTitle("choose an option");

            this.options
                .ForEach(item => this.Screen.WriteOption(item.Key, item.Value.Caption));

            var option = this.RequestOption("   option: ", this.ReadOption(this.options.ContainsKey, "invalid option! please insert [1], [2] or [3]"));
            this.options[option].Start();
        }
    }
}