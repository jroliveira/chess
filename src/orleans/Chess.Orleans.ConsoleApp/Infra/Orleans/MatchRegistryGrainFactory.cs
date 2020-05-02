namespace Chess.Orleans.ConsoleApp.Infra.Orleans
{
    using Chess.Infra.Monad;
    using Chess.Orleans.Contract;

    using global::Orleans;

    internal static class MatchRegistryGrainFactory
    {
        internal static IMatchRegistryGrain CreateMatchRegistryGrain(Option<IClusterClient> clusterClientOption) => clusterClientOption
            .Get()
            .GetGrain<IMatchRegistryGrain>("match_registry");
    }
}
