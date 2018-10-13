namespace Chess.Client.Infra.Orleans
{
    using System.Threading.Tasks;

    using Chess.Interfaces;

    using global::Orleans;

    using LightInject;

    using static Chess.Client.Infra.IoC.Container;

    internal static class PlayerFactory
    {
        internal static Task<IPlayer> CreatePlayerWith(IGrainFactory grainFactory) => grainFactory
            .CreateObjectReference<IPlayer>(GetContainer().GetInstance<IPlayer>());
    }
}
