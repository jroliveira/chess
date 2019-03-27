namespace Chess.Client.UI.Scenarios
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Convert;
    using static System.Guid;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.Orleans.MatchRegistryFactory;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.UI.GameTitle;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class SelectMatchScenario
    {
        internal static Func<IPlayer, Task<Option<IMatch>>> ShowSelectMatch(IGrainFactory grainFactory) => async player =>
        {
            ShowGameTitle();

            var matches = await CreateMatchRegistry(grainFactory).GetMatches();
            var menu = matches.ToDictionary(
                item => matches.ToList().IndexOf(item) + 1,
                _ => _);

            WriteText("----------------------- CHESS -----------------------");
            WriteText("                                                    ");

            foreach (var item in menu)
            {
                WriteText($" {item.Key} - {item.Value}");
            }

            WriteText("                                                    ");
            WriteText(" 0 - Quit                                           ");
            WriteText("                                                    ");
            WriteText("----------------------------------------------------");
            WriteText("Do you want to play? Just enter the number of the match!");
            WriteText("Select one match");

            var optionSelected = ToInt32(ReadOption(option => menu.ContainsKey(ToInt32(option)) || option == "0", "Invalid option."));
            if (optionSelected == 0)
            {
                return None;
            }

            var match = await CreateMatchWith(grainFactory, menu[optionSelected]);
            await match.JoinPlayer(player, NewGuid().ToString());

            return Some(match);
        };
    }
}
