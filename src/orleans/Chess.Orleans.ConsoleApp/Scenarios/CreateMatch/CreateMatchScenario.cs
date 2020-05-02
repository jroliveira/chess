namespace Chess.Orleans.ConsoleApp.Scenarios.CreateMatch
{
    using System.Threading.Tasks;

    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.Infra.Orleans.MatchGrainFactory;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal sealed class CreateMatchScenario : ScenarioBase
    {
        protected override async Task<GameData> DrawScenario(GameData data)
        {
            var matchName = GetFromTextBox(" Match name: ");
            var matchGrain = await CreateMatchGrain(data.ClusterClientOption, matchName);

            return data.SetMatch(Some(matchGrain));
        }
    }
}
