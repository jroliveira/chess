namespace Chess.Client.Infra.Orleans
{
    using Chess.Interfaces;

    using global::Orleans;

    internal static class MatchRegistryFactory
    {
        internal static IMatchRegistry CreateMatchRegistry(IGrainFactory grainFactory) => grainFactory.GetGrain<IMatchRegistry>("match_registry");
    }
}
