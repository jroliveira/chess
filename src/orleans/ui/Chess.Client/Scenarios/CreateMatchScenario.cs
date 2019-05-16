namespace Chess.Client.Scenarios
{
    using System;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Environment;
    using static System.String;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Symbols.Board;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class CreateMatchScenario
    {
        internal static Func<IPlayer, Task<Option<IMatch>>> ShowCreateMatch(IGrainFactory grainFactory) => async _ =>
        {
            ClearScreen();

            WriteValue($"   {Upper.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Upper.Right}{NewLine}");

            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");
            WriteValue($"   {Pipe} Create new match                    {Pipe}{NewLine}");
            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");
            WriteValue($"   {Pipe} 0 - Quit                            {Pipe}{NewLine}");
            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");

            WriteValue($"   {Middle.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Middle.Right}{NewLine}");
            WriteValue($"   {Pipe} name:~$                             {Pipe}{NewLine}");
            WriteValue($"   {Bottom.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Bottom.Right}{NewLine}");

            var name = ReadText(
                Setup,
                readText => !IsNullOrEmpty(readText),
                "Invalid match's name.");

            await CreateMatchWith(grainFactory, name);

            const string successMessage = "Match created";
            WriteInfo(successMessage);

            await Delay(1500);

            return None;

            void Setup() => SetCursor(top: 12, left: 13);
        };
    }
}
