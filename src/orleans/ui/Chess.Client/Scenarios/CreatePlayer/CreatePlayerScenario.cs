namespace Chess.Client.Scenarios.CreatePlayer
{
    using System.Threading.Tasks;

    using static Chess.Client.Infra.Orleans.PlayerFactory;
    using static Chess.Lib.Monad.Utils.Util;

    internal static partial class CreatePlayerScenario
    {
        internal static async Task<(int MenuOption, ScenarioData Data)> ShowCreatePlayer(ScenarioData data)
        {
            var (left, _, _) = DrawScenario();

            var playerName = GetPlayerName(left.Length);
            var player = await CreatePlayerWith(data.ClusterClient.Get(), playerName);

            await PrintSuccessMessage();

            return (
                9,
                new ScenarioData(
                    data.MenuOptions,
                    data.ClusterClient,
                    Some(player),
                    playerName,
                    match: None));
        }
    }
}
