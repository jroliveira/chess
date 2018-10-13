namespace Chess.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using Orleans;

    using static System.Console;
    using static System.Convert;
    using static System.Guid;
    using static System.Threading.Tasks.Task;

    using static Chess.Client.Infra.Orleans.MatchFactory;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class MenuOption
    {
        internal static IReadOnlyDictionary<int, Func<IPlayer, Task<Option<IMatch>>>> GetMenuOption(IGrainFactory grainFactory) => new Dictionary<int, Func<IPlayer, Task<Option<IMatch>>>>
        {
            { 1, CreateMatch(grainFactory) },
            { 2, SelectMatch(grainFactory) },
        };

        private static Func<IPlayer, Task<Option<IMatch>>> CreateMatch(IGrainFactory grainFactory) => async _ =>
        {
            await CreateMatchWith(grainFactory, RequestOption("What is the name of the match?"));

            Clear();
            WriteLine("Match created!");

            await Delay(1000);

            return None;
        };

        private static Func<IPlayer, Task<Option<IMatch>>> SelectMatch(IGrainFactory grainFactory) => async player =>
        {
            Clear();

            var matchRegistry = grainFactory.GetGrain<IMatchRegistry>("match_registry");
            var matches = await matchRegistry.GetMatches();

            PrintMenu(matches);

            var option = ToInt32(RequestOption("Select one match:"));
            if (option == 0)
            {
                return None;
            }

            var match = await CreateMatchWith(grainFactory, matches.ElementAt(option - 1));
            await match.JoinPlayer(player, NewGuid().ToString());

            return Some(match);
        };

        private static void PrintMenu(IReadOnlyCollection<string> matches)
        {
            for (var i = 0; i < matches.Count; i++)
            {
                WriteLine($"{i + 1} - {matches.ElementAt(i)}");
            }

            WriteLine();
            WriteLine("0 - Quit");
            WriteLine();
            WriteLine("Do you want to play? Just enter the number of the match!");
            WriteLine();
        }

        private static string RequestOption(string title)
        {
            WriteLine(title);

            return ReadLine();
        }
    }
}
