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
    using static Chess.Client.Infra.UI.Symbols.Board;
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

                WriteValue($"   {Upper.Left}");
                WriteValue(Dash, times: 37);
                WriteValue($"{Upper.Right}{NewLine}");

                WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");
                WriteValue($"   {Pipe} 1 - Create match                    {Pipe}{NewLine}");
                WriteValue($"   {Pipe} 2 - List matches                    {Pipe}{NewLine}");
                WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");
                WriteValue($"   {Pipe} 0 - Quit                            {Pipe}{NewLine}");
                WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");

                WriteValue($"   {Middle.Left}");
                WriteValue(Dash, times: 37);
                WriteValue($"{Middle.Right}{NewLine}");
                WriteValue($"   {Pipe} option:~$                           {Pipe}{NewLine}");
                WriteValue($"   {Bottom.Left}");
                WriteValue(Dash, times: 37);
                WriteValue($"{Bottom.Right}{NewLine}");

                var option = ToInt32(RequestOption(Setup).ToString());

                match = await Options[option](grainFactory)(player);
            }
            while (!match.IsDefined);

            await done(match.Get());

            void Setup() => SetCursor(top: 13, left: 15);
        }

        private static Func<IPlayer, Task<Option<IMatch>>> ExitProgram(IGrainFactory grainFactory)
        {
            Exit(0);
            return null;
        }

        private static char RequestOption(Action setup) => ReadChar(
            setup,
            readValue => TryParse(readValue.ToString(), out var option) && Options.ContainsKey(option),
            "Invalid option.");
    }
}
