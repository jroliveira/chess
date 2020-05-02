namespace Chess.Orleans.ConsoleApp.Scenarios.Shared
{
    using System.Threading.Tasks;

    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.Orleans.ConsoleApp.GameMenuItem;
    using static Chess.Orleans.ConsoleApp.Infra.UI.Terminal;

    internal abstract class ScenarioBase
    {
        internal Task<GameData> ShowScenario(GameData data)
        {
            ClearScreen();
            SetCursorPosition(top: 10);

            return this.DrawScenario(data);
        }

        protected virtual Task<GameData> DrawScenario(GameData data) => Task(data.SetGameMenuItem(Default));
    }
}
