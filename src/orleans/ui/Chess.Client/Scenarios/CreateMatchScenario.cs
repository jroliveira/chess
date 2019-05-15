namespace Chess.Client.Scenarios
{
    using System;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.String;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Client.Infra.UI.Reader;
    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Client.Infra.UI.WriterBox;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class CreateMatchScenario
    {
        internal static Func<IPlayer, Task<Option<IMatch>>> ShowCreateMatch(IGrainFactory grainFactory) => async _ =>
        {
            ClearScreen();

            WriteTextWithNewLine("   ╔═══════════════ CHESS ═══════════════╗");
            WriteTextWithNewLine("   ║                                     ║");
            WriteTextWithNewLine("   ║ Create new match                    ║");
            WriteTextWithNewLine("   ║                                     ║");
            WriteTextWithNewLine("   ║ 0 - Quit                            ║");
            WriteTextWithNewLine("   ║                                     ║");
            WriteTextWithNewLine("   ╠═════════════════════════════════════╣");
            WriteTextWithNewLine("   ║ name:~$                             ");
            WriteTextWithNewLine("   ╚═════════════════════════════════════╝");

            var name = ReadText(
                Setup,
                readText => !IsNullOrEmpty(readText),
                "invalid match's name");

            await CreateMatchWith(grainFactory, name);

            const string successMessage = "match created";
            WriteBox(successMessage.Length, () => WriteValue(successMessage));

            await Delay(1500);

            return None;

            void Setup() => SetCursor(top: 10, left: 13);
        };
    }
}
