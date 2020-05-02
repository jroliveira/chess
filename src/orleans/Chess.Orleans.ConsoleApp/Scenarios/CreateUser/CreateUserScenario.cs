namespace Chess.Orleans.ConsoleApp.Scenarios.CreateUser
{
    using System.Threading.Tasks;

    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.Infra.Orleans.UserGrainFactory;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal sealed class CreateUserScenario : ScenarioBase
    {
        protected override async Task<GameData> DrawScenario(GameData data)
        {
            var userName = GetFromTextBox(" User name: ");
            var userGrain = await CreateUserGrain(data.ClusterClientOption, userName);

            return data.SetUser(Some(userGrain), Some(userName));
        }
    }
}
