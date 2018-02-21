namespace Chess.Client.Infra
{
    using System.Threading.Tasks;
    using Chess.Interfaces;
    using LightInject;
    using Orleans;
    using Orleans.Runtime;
    using static Chess.Client.Infra.IoC.Container;
    using static Chess.Client.Infra.UI.Writer;
    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;

    internal static class GameServerFactory
    {
        private const int AttemptsBeforeFailing = 5;

        public static async Task<IGameServer> ConnectAndGetGameServer()
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

        private static async Task<IGameServer> GetGameServer(IGrainFactory client)
        {
            var gameClientInstance = GetContainer().GetInstance<IGameClient>();
            var gameClient = await client.CreateObjectReference<IGameClient>(gameClientInstance).ConfigureAwait(false);

            var gameServer = client.GetGrain<IGameServer>("game-test");
            await gameServer.Subscribe(gameClient).ConfigureAwait(false);

            return gameServer;
        }
    }
}
