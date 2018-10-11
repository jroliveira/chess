namespace Chess.Interfaces
{
    using Chess.Lib.Monad;
    using Chess.Models;

    using Orleans;

    public interface IGameClient : IGrainObserver
    {
        void GameChanged(Try<Chessboard> chessboard, IGameServer gameServer);

        void SetPlayer(Option<string> playerName);
    }
}
