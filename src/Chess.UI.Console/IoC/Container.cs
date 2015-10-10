namespace Chess.UI.Console.IoC
{
    using Chess.Multiplayer;
    using Chess.UI.Console.Libs;
    using Chess.UI.Console.Libs.Match;
    using Chess.UI.Console.Scenarios;
    using Chess.UI.Console.Scenarios.Matches;
    using Chess.UI.Console.Scenarios.Multiplayer;

    using LightInject;

    public class Container
    {
        private static readonly IServiceContainer ServiceContainer;

        static Container()
        {
            ServiceContainer = new ServiceContainer();

            ServiceContainer.Register<IGameMultiplayer, GameMultiplayer>(new PerContainerLifetime());

            ServiceContainer.Register<Main>();
            ServiceContainer.Register<Client>();
            ServiceContainer.Register<Server>();
            ServiceContainer.Register<Offline>();
            ServiceContainer.Register<Online>();

            ServiceContainer.Register<IScreen, Screen>();
            ServiceContainer.Register<IWriter, Libs.Writer>();
            ServiceContainer.Register<IReader, Reader>();

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
