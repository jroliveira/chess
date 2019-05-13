namespace Chess.Client.Scenarios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Convert;
    using static System.Environment;
    using static System.Guid;
    using static System.Int32;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.Orleans.MatchRegistryFactory;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Symbols.Board;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class SelectMatchScenario
    {
        internal static Func<IPlayer, Task<Option<IMatch>>> ShowSelectMatch(IGrainFactory grainFactory) => async player =>
        {
            var matches = await CreateMatchRegistry(grainFactory).GetMatches();
            var menu = matches.ToDictionary(
                item => matches.ToList().IndexOf(item) + 1,
                _ => _);

            ClearScreen();

            WriteValue($"   {Upper.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Upper.Right}{NewLine}");

            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");

            foreach (var item in menu)
            {
                WriteValue($"   {Pipe} {item.Key} - {item.Value}");
                WriteValue(' ', times: 32 - item.Value.Length);
                WriteValue($"{Pipe}{NewLine}");
            }

            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");
            WriteValue($"   {Pipe} 0 - Quit                            {Pipe}{NewLine}");
            WriteValue($"   {Pipe}                                     {Pipe}{NewLine}");

            WriteValue($"   {Middle.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Middle.Right}{NewLine}");
            WriteValue($"   {Pipe} match:~$                            {Pipe}{NewLine}");
            WriteValue($"   {Bottom.Left}");
            WriteValue(Dash, times: 37);
            WriteValue($"{Bottom.Right}{NewLine}");

            var option = ToInt32(RequestMatch(menu, Setup).ToString());

            if (option == 0)
            {
                return None;
            }

            var match = await CreateMatchWith(grainFactory, menu[option]);
            await match.JoinPlayer(player, NewGuid().ToString());

            return Some(match);

            void Setup() => SetCursor(top: 11 + menu.Count, left: 14);
        };

        private static char RequestMatch(IReadOnlyDictionary<int, string> menu, Action setup) => ReadChar(
            setup,
            readValue => TryParse(readValue.ToString(), out int option) && menu.ContainsKey(option),
            "Invalid option.");
    }
}
