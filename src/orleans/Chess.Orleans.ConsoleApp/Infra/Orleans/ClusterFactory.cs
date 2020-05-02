namespace Chess.Orleans.ConsoleApp.Infra.Orleans
{
    using System;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;

    using global::Orleans;
    using global::Orleans.Runtime;

    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;

    using static Chess.Infra.Monad.Utils.Util;

    internal static class ClusterFactory
    {
        private const int AttemptsBeforeFailing = 5;
        private const int WaitingTimeInSec = 4;

        internal static async Task<Try<IClusterClient>> CreateCluster(Action<int, int> writeMessage)
        {
            var attempt = 0;

            while (true)
            {
                attempt++;
                writeMessage(attempt, AttemptsBeforeFailing);

                try
                {
                    var client = new ClientBuilder()
                        .UseLocalhostClustering()
                        .Build();

                    await client.Connect();

                    return Success(client);
                }
                catch (SiloUnavailableException exception)
                {
                    if (attempt >= AttemptsBeforeFailing)
                    {
                        return new ChessException(exception.Message);
                    }

                    await Delay(FromSeconds(WaitingTimeInSec));
                }
                catch (Exception exception)
                {
                    return new ChessException(exception.Message);
                }
            }
        }
    }
}
