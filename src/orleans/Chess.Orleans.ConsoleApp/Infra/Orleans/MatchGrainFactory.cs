namespace Chess.Orleans.ConsoleApp.Infra.Orleans
{
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Extensions;
    using Chess.Orleans.Contract;

    using global::Orleans;

    internal static class MatchGrainFactory
    {
        internal static async Task<IMatchGrain> CreateMatchGrain(Option<IClusterClient> clusterClientOption, Option<string> nameOption)
        {
            var matchGrain = clusterClientOption
                .Get()
                .GetGrain<IMatchGrain>(nameOption.GetOrElse("default"));

            await matchGrain.WakeUp();

            return matchGrain;
        }
    }
}
