namespace Chess.Multiplayer
{
    using Chess.Multiplayer.EventHandlers;

    public interface IGameMultiplayer : IGame
    {
        event ErrorEventHandler Error;

        event PlayedEventHandler Played;

        event ConnectedEventHandler Connected;

        void Connect(string ipAddress, string port);

        void WaitingForOpponent();

        void WaitingTheMove();
    }
}
