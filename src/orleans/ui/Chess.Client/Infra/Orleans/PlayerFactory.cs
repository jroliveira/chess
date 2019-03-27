namespace Chess.Client.Infra.Orleans
{
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using global::Orleans;

    internal static class PlayerFactory
    {
        internal static Task<IPlayer> CreatePlayerWith(IGrainFactory grainFactory) => grainFactory.CreateObjectReference<IPlayer>(new Player());
    }
}
