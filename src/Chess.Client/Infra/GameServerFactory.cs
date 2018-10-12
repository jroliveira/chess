namespace Chess.Client.Infra
{
    using System;
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using LightInject;

    using Orleans;
    using Orleans.Runtime;
    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;
    using static Chess.Client.Infra.IoC.Container;
    using static Chess.Client.Infra.UI.Writer;

    using static System.Guid;
    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;
    using static Chess.Client.Infra.IoC.Container;
    using static Chess.Client.Infra.UI.Writer;

    internal static class GameServerFactory
    {
        private const int AttemptsBeforeFailing = 5;

        public static async Task<IMatch> ConnectAndGetGameServer()
        {
            var attempt = 0;

            while (true)
            {
                try
                {
                    var client = await Connect().ConfigureAwait(false);
                    return await GetGameServer(client).ConfigureAwait(false);
                }
                catch (SiloUnavailableException)
                {
                    attempt++;

                    WriteValue("Attempt {0} of {1} failed to initialize the client.", attempt, AttemptsBeforeFailing);

                    if (attempt > AttemptsBeforeFailing)
                    {
                        throw;
                    }

                    await Delay(FromSeconds(4)).ConfigureAwait(false);
                }
            }
        }

        private static async Task<IClusterClient> Connect()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();

            await client.Connect().ConfigureAwait(false);

            return client;
        }

        private static async Task<IMatch> GetGameServer(IGrainFactory client)
        {
            var playerName = NewGuid().ToString();
            var playerInstance = GetContainer().GetInstance<IPlayerCallback>();
            var player = await client.CreateObjectReference<IPlayerCallback>(playerInstance).ConfigureAwait(false);
            player.SetPlayer(playerName);

            var gameServer = client.GetGrain<IMatch>("game-test");
            await gameServer.WakeUp();

            var gameStarted = false;

            do
            {
                Console.Clear();

                var selectedMainOption = PrintMenu();
                if (selectedMainOption == 1)
                {
                    Console.WriteLine("What is the name of the game?");
                    await client.GetGrain<IMatch>(Console.ReadLine()).WakeUp();

                    Console.Clear();
                    Console.WriteLine("Game created!");
                    await Task.Delay(1000);
                }
                else if (selectedMainOption == 2)
                {
                    Console.Clear();
                    var allMatchesRegistry = client.GetGrain<IMatchRegistry>("match_registry");

                    var matches = await allMatchesRegistry.GetAllMatches().ConfigureAwait(false);

                    for (var i = 1; i < matches.Count; i++)
                    {
                        Console.WriteLine($"{i} - {matches[i].GetPrimaryKeyString()}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("0 - Quit");
                    Console.WriteLine();
                    Console.WriteLine("Do you want to play? Just enter the number of the match!");
                    Console.WriteLine();
                    Console.WriteLine("Select one option:");

                    var matchSelected = Convert.ToInt32(Console.ReadLine());

                    if (matchSelected != 0)
                    {
                        gameServer = client.GetGrain<IMatch>(matches[matchSelected - 1].GetPrimaryKeyString());
                        await gameServer.JoinAsync(player).ConfigureAwait(false);
                        gameStarted = true;
                    }
                }
            } while (!gameStarted);

            return gameServer;
        }

        private static int PrintMenu()
        {
            Console.WriteLine("                      _._   +");
            Console.WriteLine("                  ,   ( )  ( )   ,");
            Console.WriteLine("        [UU] T\\  (^)  / \\  / \\  (^)  /T [UU]");
            Console.WriteLine("     ()  ||  |\\) / \\  | |  | |  / \\ (/|  ||  ()");
            Console.WriteLine("     {}  {}  {}  { }  { }  { }  { }  {}  {}  {}");
            Console.WriteLine("    {__}{__}{__}{___}{___}{___}{___}{__}{__}{__}");
            Console.WriteLine();
            Console.WriteLine("------------------------CHESS------------------------");
            Console.WriteLine("|                                                   |");
            Console.WriteLine("| 1 - Create game                                   |");
            Console.WriteLine("| 2 - List all games                                |");
            Console.WriteLine("|                                                   |");
            Console.WriteLine("| 0 - Quit                                          |");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Select one option:");

            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
