namespace Chess.Orleans.ConsoleApp.Scenarios.MainMenu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Orleans.ConsoleApp.Scenarios.Shared;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal sealed class MainMenuScenario : ScenarioBase
    {
        protected override Task<GameData> DrawScenario(GameData data)
        {
            var option = GetFromSelectBox(" Menu option: ", new List<(int, string)>
            {
                (1, "Create a new match"),
                (2, "List all matches"),
            });

            return Task(data.SetGameOption(option));
        }
    }
}
