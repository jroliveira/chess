using Chess.Multiplayer.EventHandlers;

namespace Chess.Multiplayer
{
    public interface IGameMultiplayer : IGame
    {
        void Connect(string ipAddress, string port);
        void WaitingForOpponent();
        void WaitingTheMove();

        event ErrorEventHandler Error;
        event PlayedEventHandler Played;
        event ConnectedEventHandler Connected;
    }
}
