namespace Chess.Client.UI.Scenarios
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
    using static Chess.Client.UI.GameTitle;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class CreateMatchScenario
    {
        internal static Func<IPlayer, Task<Option<IMatch>>> ShowCreateMatch(IGrainFactory grainFactory) => async _ =>
        {
            ShowGameTitle();
            WriteText("What is the name of the match?");
            var optionSelected = ReadOption(option => !IsNullOrEmpty(option), "Invalid name of the match.");

            await CreateMatchWith(grainFactory, optionSelected);

            WriteValue("Match created!");

            await Delay(1000);

            return None;
        };
    }
}
