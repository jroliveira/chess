namespace Chess.Client.Scenarios.CreateMatch
{
    using System.Threading.Tasks;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Lib.Monad.Utils.Util;

    internal static partial class CreateMatchScenario
    {
        internal static async Task<(int MenuOption, ScenarioData Data)> ShowCreateMatch(ScenarioData data)
        {
            var (left, _, _) = DrawScenario();

            var matchName = GetMatchName(left.Length);
            var match = await CreateMatchWith(data.ClusterClient.Get(), matchName);
            await match.JoinPlayer(data.Player.Get(), data.PlayerName.Get());

            await PrintSuccessMessage();

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
