using System;
using System.Collections.Generic;
using Chess.UI.Console.Scenarios.MultiplayerScenarios;

namespace Chess.UI.Console.Scenarios
{
    public class Multiplayer : Scenario
    {
        private readonly IDictionary<char, Action> _options;

        public Multiplayer(ChessGame game)
            : base(game)
        {
            var connect = new Connect(Game);
            var waiting = new Waiting(Game);

            _options = new Dictionary<char, Action>
            {
                { '1', waiting.Start },
                { '2', connect.Start }
            };
        }

        protected override void Show()
        {
            Text.WriteInsideTheBox("choose an option");
            Text.NewLine();
            Text.NewLine();
            Text.WriteOption("1", "waiting for opponent");
            Text.NewLine();
            Text.WriteOption("2", "connect an one opponent");
            Text.NewLine();
            Text.NewLine();
            Text.WriteWithSleep("   {0} option: ", ArrowRight);

            var option = ReadOption();
            _options[option]();
        }

        private char ReadOption()
        {
            bool isValid;
            char option;

            do
            {
                option = System.Console.ReadKey().KeyChar;
                isValid = _options.ContainsKey(option);

                if (!isValid)
                {
                    Text.Error("invalid option! please insert 1 or 2");
                }
            } while (!isValid);

            return option;
        }
    }
}