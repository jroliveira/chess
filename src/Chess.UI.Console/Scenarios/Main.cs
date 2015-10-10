using System;
using System.Collections.Generic;
using Chess.Multiplayer;
using Chess.UI.Console.Libs;
using Chess.UI.Console.Scenarios.Matches;
using Chess.UI.Console.Scenarios.Multiplayer;

namespace Chess.UI.Console.Scenarios
{
    public class Main : Scenario
    {
        private readonly IDictionary<char, Action> _options;

        public Main(IGameMultiplayer game, Offline match, Client client, Server server, IWriter writer, IReader reader, IScreen screen)
            : base(game, writer, reader, screen)
        {
            screen.Title = "Chess";
            screen.Size = new Size { Height = 25, Width = 48 };

            _options = new Dictionary<char, Action>
            {
                { '1', match.Start },
                { '2', server.Start },
                { '3', client.Start }
            };
        }

        public void Start()
        {
            Setup();

            Writer.WriteInsideTheBox("choose an option");
            Writer.NewLine();
            Writer.NewLine();

            Writer.WriteOption("1", "solo");
            Writer.NewLine();
            Writer.WriteOption("2", "waiting for opponent");
            Writer.NewLine();
            Writer.WriteOption("3", "connect an one opponent");
            Writer.NewLine();
            Writer.WriteWithSleep("   option: ");

            var option = ReadOption();
            _options[option]();
        }

        private char ReadOption()
        {
            bool isValid;
            char option;

            do
            {
                option = Reader.ReadKey();
                isValid = _options.ContainsKey(option);

                if (!isValid)
                {
                    Writer.WriteError("invalid option! please insert [1], [2] or [3]");
                }
            } while (!isValid);

            return option;
        }
    }
}