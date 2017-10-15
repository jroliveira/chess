namespace Chess.Terminal.IoC
{
    using Chess.Multiplayer;
    using Chess.Terminal.Lib;
    using Chess.Terminal.Lib.Match;
    using Chess.Terminal.Scenarios;
    using Chess.Terminal.Scenarios.Matches;
    using Chess.Terminal.Scenarios.Multiplayer;

    using LightInject;

    public class Container
    {
        private static readonly IServiceContainer ServiceContainer;

        static Container()
        {
            ServiceContainer = new ServiceContainer();

            ServiceContainer.Register<IGame, Game>(new PerContainerLifetime());
            ServiceContainer.Register<IGameMultiplayer, GameMultiplayer>(new PerContainerLifetime());

            ServiceContainer.Register<Main>();
            ServiceContainer.Register<Client>();
            ServiceContainer.Register<Server>();
            ServiceContainer.Register<Offline>();
            ServiceContainer.Register<Online>();

            ServiceContainer.Register<IScreen, Screen>();

            ServiceContainer.Register<Chessboard>();
        }

        public static IServiceContainer GetContainer()
        {
            return ServiceContainer;
        }

        public static T GetInstance<T>()
        {
            return ServiceContainer.GetInstance<T>();
        }
    }
}
