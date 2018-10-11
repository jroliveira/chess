namespace Chess.Interfaces
{
    using Chess.Lib.Monad;
    using Chess.Models;

    using Orleans;

    public interface IPlayerCallback : IGrainObserver
    {
        void GameChanged(Try<Chessboard> chessboard);

        void YourMove(IBoard board);

        void SetPlayer(Option<string> playerName);
    }
}
