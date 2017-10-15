namespace Chess.Multiplayer
{
    using System;

    public interface IGameMultiplayer : IGame
    {
        event Action<Exception> Error;

        event Action Connected;

        event Action<string, string> Played;

        void Connect(string ipAddress, string port);

        void WaitingForOpponent();

        void WaitingTheMove();
    }
}
