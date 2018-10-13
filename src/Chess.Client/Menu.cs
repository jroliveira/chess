namespace Chess.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Interfaces;
    using Chess.Lib.Monad;

    using static System.Console;
    using static System.Convert;
    using static System.String;

    internal class Menu
    {
        private readonly IPlayer player;

        public Menu(IPlayer player) => this.player = player;

        public async Task Show(IReadOnlyDictionary<int, Func<IPlayer, Task<Option<IMatch>>>> options, Func<IMatch, Task> done)
        {
            Option<IMatch> match = null;

            do
            {
                Clear();
                PrintMenu();

                if (options.TryGetValue(RequestOption(), out var executeOption))
                {
                    match = await executeOption(this.player);
                }
            } while (!match.IsDefined);

            await done(match.Get());
        }

        private static void PrintMenu()
        {
            WriteLine("                      _._   +");
            WriteLine("                  ,   ( )  ( )   ,");
            WriteLine("        [UU] T\\  (^)  / \\  / \\  (^)  /T [UU]");
            WriteLine("     ()  ||  |\\) / \\  | |  | |  / \\ (/|  ||  ()");
            WriteLine("     {}  {}  {}  { }  { }  { }  { }  {}  {}  {}");
            WriteLine("    {__}{__}{__}{___}{___}{___}{___}{__}{__}{__}");
            WriteLine();
            WriteLine("----------------------- CHESS -----------------------");
            WriteLine("|                                                   |");
            WriteLine("| 1 - Create match                                  |");
            WriteLine("| 2 - List matches                                  |");
            WriteLine("|                                                   |");
            WriteLine("| 0 - Quit                                          |");
            WriteLine("|                                                   |");
            WriteLine("----------------------------------------------------");
            WriteLine(Empty);
        }

        private static int RequestOption()
        {
            WriteLine("Select one option:");

            return ToInt32(ReadLine());
        }
    }
}
