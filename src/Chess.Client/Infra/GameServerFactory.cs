namespace Chess.Client.Infra
{
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using LightInject;

    using Orleans;
    using Orleans.Runtime;

    using static System.Guid;
    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;
    using static Chess.Client.Infra.IoC.Container;
    using static Chess.Client.Infra.UI.Writer;

    internal static class GameServerFactory
    {
        private const int AttemptsBeforeFailing = 5;

        public static async Task<IBoard> ConnectAndGetGameServer()
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

        private static async Task<IBoard> GetGameServer(IGrainFactory client)
        {
            var playerName = NewGuid().ToString();
            var playerInstance = GetContainer().GetInstance<IPlayerCallback>();
            var player = await client.CreateObjectReference<IPlayerCallback>(playerInstance).ConfigureAwait(false);
            player.SetPlayer(playerName);

            var gameServer = client.GetGrain<IBoard>("game-test");
            await gameServer.JoinAsync(player).ConfigureAwait(false);

            return gameServer;
        }
    }
}
