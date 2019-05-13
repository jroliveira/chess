namespace Chess.Client.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Convert;
    using static System.Environment;
    using static System.Int32;

    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.Scenarios.CreateMatchScenario;
    using static Chess.Client.Scenarios.SelectMatchScenario;

    internal static class MainMenuScenario
    {
        private static readonly IReadOnlyDictionary<int, Func<IGrainFactory, Func<IPlayer, Task<Option<IMatch>>>>> Options = new Dictionary<int, Func<IGrainFactory, Func<IPlayer, Task<Option<IMatch>>>>>
        {
            { 1, ShowCreateMatch },
            { 2, ShowSelectMatch },
            { 0, ExitProgram },
        };

        internal static async Task ShowMainMenu(IPlayer player, IGrainFactory grainFactory, Func<IMatch, Task> done)
        {
            Option<IMatch> match;

            do
            {
                ClearScreen();

                WriteTextWithNewLine("   ╔═══════════════ CHESS ═══════════════╗");
                WriteTextWithNewLine("   ║                                     ║");
                WriteTextWithNewLine("   ║ 1 - Create match                    ║");
                WriteTextWithNewLine("   ║ 2 - List matches                    ║");
                WriteTextWithNewLine("   ║                                     ║");
                WriteTextWithNewLine("   ║ 0 - Quit                            ║");
                WriteTextWithNewLine("   ║                                     ║");
                WriteTextWithNewLine("   ╠═════════════════════════════════════╣");
                WriteTextWithNewLine("   ║ option:~$                           ║");
                WriteTextWithNewLine("   ╚═════════════════════════════════════╝");

                var option = ToInt32(RequestOption(Setup).ToString());

                match = await Options[option](grainFactory)(player);
            }
            while (!match.IsDefined);

            await done(match.Get());

            void Setup() => SetCursor(top: 11, left: 15);
        }

        private static Func<IPlayer, Task<Option<IMatch>>> ExitProgram(IGrainFactory grainFactory)
        {
            Exit(0);
            return null;
        }

        private static char RequestOption(Action setup) => ReadChar(
            setup,
            readValue => TryParse(readValue.ToString(), out var option) && Options.ContainsKey(option),
            "invalid option");
    }
}
