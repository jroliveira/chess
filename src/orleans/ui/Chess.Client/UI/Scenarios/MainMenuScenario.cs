namespace Chess.Client.UI.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Convert;

    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.UI.GameTitle;
    using static Chess.Client.UI.Scenarios.CreateMatchScenario;
    using static Chess.Client.UI.Scenarios.SelectMatchScenario;

    internal static class MainMenuScenario
    {
        private static readonly IReadOnlyDictionary<int, Func<IGrainFactory, Func<IPlayer, Task<Option<IMatch>>>>> Options = new Dictionary<int, Func<IGrainFactory, Func<IPlayer, Task<Option<IMatch>>>>>
        {
            { 1, ShowCreateMatch },
            { 2, ShowSelectMatch },
        };

        internal static async Task ShowMainMenu(IPlayer player, IGrainFactory grainFactory, Func<IMatch, Task> done)
        {
            Option<IMatch> match = null;

            do
            {
                ShowGameTitle();

                WriteText("----------------------- CHESS -----------------------");
                WriteText("                                                     ");
                WriteText(" 1 - Create match                                    ");
                WriteText(" 2 - List matches                                    ");
                WriteText("                                                     ");
                WriteText(" 0 - Quit                                            ");
                WriteText("                                                     ");
                WriteText("-----------------------------------------------------");
                WriteText("Select one option:");

                var optionSelected = ToInt32(ReadOption(option => Options.ContainsKey(ToInt32(option)), "Invalid option."));

                if (Options.TryGetValue(optionSelected, out var executeOption))
                {
                    match = await executeOption(grainFactory)(player);
                }
            }
            while (!match.IsDefined);

            await done(match.Get());
        }
    }
}
