namespace Chess.Orleans.ConsoleApp.Scenarios.SelectMatch
{
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Orleans.ConsoleApp.Infra.UI;
    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.GameMenuItem;
    using static Chess.Orleans.ConsoleApp.Infra.Orleans.MatchGrainFactory;
    using static Chess.Orleans.ConsoleApp.Infra.Orleans.MatchRegistryGrainFactory;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal sealed class SelectMatchScenario : ScenarioBase
    {
        protected override async Task<GameData> DrawScenario(GameData data)
        {
            var matches = await CreateMatchRegistryGrain(data.ClusterClientOption).GetMatches();
            SelectOptions options = matches.ToList();

            var option = GetFromSelectBox(" Match number: ", options);
            if (option == 0)
            {
                return data.SetGameMenuItem(MainMenu);
            }

            var matchGrain = await CreateMatchGrain(data.ClusterClientOption, options[option]);

            return data.SetMatch(Some(matchGrain));
        }
    }
}
