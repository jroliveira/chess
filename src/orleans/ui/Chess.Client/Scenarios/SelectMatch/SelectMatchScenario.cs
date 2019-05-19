namespace Chess.Client.Scenarios.SelectMatch
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static System.String;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.Orleans.MatchRegistryFactory;
    using static Chess.Lib.Monad.Utils.Util;

    internal static partial class SelectMatchScenario
    {
        internal static async Task<(int MenuOption, ScenarioData Data)> ShowSelectMatch(ScenarioData data)
        {
            var menuTmp = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(-1, Empty) };

            var matches = (await CreateMatchRegistry(data.ClusterClient.Get()).GetMatches()).ToList();
            if (matches.Any())
            {
                menuTmp.AddRange(matches.Select(item => new KeyValuePair<int, string>(matches.ToList().IndexOf(item) + 1, item)));
                menuTmp.Add(new KeyValuePair<int, string>(-2, Empty));
            }

            menuTmp.Add(new KeyValuePair<int, string>(0, "Back to main menu"));
            menuTmp.Add(new KeyValuePair<int, string>(-3, Empty));

            var menu = menuTmp.ToDictionary(
                item => item.Key,
                item => item.Value);

            var (left, _, _) = DrawScenario(menu);

            var option = GetOption(left.Length, menu);
            if (option == 0)
            {
                return (09, data);
            }

            var match = await CreateMatchWith(data.ClusterClient.Get(), menu[option]);
            await match.JoinPlayer(data.Player.Get(), data.PlayerName.Get());

            return (
                99,
                new ScenarioData(
                    data.MenuOptions,
                    data.ClusterClient,
                    data.Player,
                    data.PlayerName,
                    Some(match)));
        }
    }
}
