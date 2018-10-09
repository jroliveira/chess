namespace Chess.Interfaces
{
    using Chess.Models;
    using Orleans;

    public interface IGameClient : IGrainObserver
    {
        void GameChanged(Chessboard chessboard, IGameServer gameServer);
    }
}