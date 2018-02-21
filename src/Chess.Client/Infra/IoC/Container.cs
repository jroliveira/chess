namespace Chess.Client.Infra.IoC
{
    using Chess.Interfaces;
    using LightInject;

    internal static class Container
    {
        private static readonly IServiceContainer ServiceContainer;

        static Container()
        {
            ServiceContainer = new ServiceContainer();

            ServiceContainer.Register<IGameClient, GameClient>(new PerContainerLifetime());
            ServiceContainer.Register<Main>();
        }

        public static IServiceContainer GetContainer() => ServiceContainer;
    }
}
