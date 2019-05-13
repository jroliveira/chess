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
    using static System.Guid;
    using static System.Int32;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.Orleans.MatchRegistryFactory;
    using static Chess.Client.Infra.UI.Reader;
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

            WriteTextWithNewLine("   ╔═══════════════ CHESS ═══════════════╗");
            WriteTextWithNewLine("   ║                                     ║");

            foreach (var item in menu)
            {
                WriteValue($"   ║ {item.Key} - {item.Value}");

                for (var i = 0; i < 32 - item.Value.Length; i++)
                {
                    WriteValue(' ');
                }

                WriteTextWithNewLine("║");
            }

            WriteTextWithNewLine("   ║                                     ║");
            WriteTextWithNewLine("   ║ 0 - Quit                            ║");
            WriteTextWithNewLine("   ║                                     ║");
            WriteTextWithNewLine("   ╠═════════════════════════════════════╣");
            WriteTextWithNewLine("   ║ match:~$                            ║");
            WriteTextWithNewLine("   ╚═════════════════════════════════════╝");

            var option = ToInt32(RequestMatch(menu, Setup).ToString());

            if (option == 0)
            {
                return None;
            }

            var match = await CreateMatchWith(grainFactory, menu[option]);
            await match.JoinPlayer(player, NewGuid().ToString());

            return Some(match);

            void Setup() => SetCursor(top: 9 + menu.Count, left: 14);
        };

        private static char RequestMatch(IReadOnlyDictionary<int, string> menu, Action setup) => ReadChar(
            setup,
            readValue => TryParse(readValue.ToString(), out int option) && menu.ContainsKey(option),
            "invalid option");
    }
}
