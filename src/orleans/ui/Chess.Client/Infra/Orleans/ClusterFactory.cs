namespace Chess.Client.Infra.Orleans
{
    using System;
    using System.Threading.Tasks;

    using Chess.Lib.Monad;

    using global::Orleans;
    using global::Orleans.Runtime;

    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;

    using static Chess.Lib.Monad.Utils.Util;

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
                        return exception;
                    }

                    await Delay(FromSeconds(WaitingTimeInSec));
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }
        }
    }
}
