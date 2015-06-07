using System;
using System.Collections.Generic;

namespace Chess.UI.Console.Scenarios
{
    public class Main : Scenario
    {
        private readonly IDictionary<char, Action> _options;

        public Main()
            : base(new ChessGame())
        {
            System.Console.Title = "Chess";
            System.Console.SetWindowSize(84, 57);

            var match = new Match(Game);
            var multiplayer = new Multiplayer(Game);

            _options = new Dictionary<char, Action>
            {
                { '1', match.Start },
                { '2', multiplayer.Start }
            };
        }

        protected override void Show()
        {
            Text.WriteInsideTheBox("choose an option");
            Text.NewLine();
            Text.NewLine();

            Text.WriteOption("1", "solo");
            Text.NewLine();
            Text.WriteOption("2", "multiplayer");
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