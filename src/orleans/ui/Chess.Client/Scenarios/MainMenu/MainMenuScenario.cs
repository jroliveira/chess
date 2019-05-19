namespace Chess.Client.Scenarios.MainMenu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static System.String;
    using static System.Threading.Tasks.Task;

    internal static partial class MainMenuScenario
    {
        private static readonly IReadOnlyDictionary<int, string> Menu = new Dictionary<int, string>
        {
            { -1, Empty },
            { 1, "Create a new match" },
            { 2, "List all matches" },
            { -2, Empty },
            { 0, "Close the game" },
            { -3, Empty },
        };

        internal static Task<(int MenuOption, ScenarioData Data)> ShowMainMenu(ScenarioData data)
        {
            var (left, _, _) = DrawScenario(Menu);

            var option = GetOption(left.Length, Menu);

            return FromResult((
                option,
                data));
        }
    }
}
