namespace Chess.Client.Infra.Orleans
{
    using System;
    using System.Threading.Tasks;

    using Chess.Lib.Monad;

    using global::Orleans;
    using global::Orleans.Runtime;

    using static System.Environment;
    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;

    using static Chess.Client.Infra.UI.Writer;
    using static Chess.Lib.Monad.Utils.Util;

    internal static class ClusterFactory
    {
        private const int AttemptsBeforeFailing = 5;

        internal static async Task<Try<IClusterClient>> CreateCluster()
        {
            var attempt = 0;

            while (true)
            {
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
                    if (attempt > AttemptsBeforeFailing)
                    {
                        return exception;
                    }

                    attempt++;

                    WriteValue($"Attempt {attempt} of {AttemptsBeforeFailing} failed to initialize the client. {NewLine}");

                    await Delay(FromSeconds(4));
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }
        }
    }
}
