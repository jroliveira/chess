namespace Chess.Interfaces
{
    using Chess.Lib.Monad;
    using Chess.Models;

    using Orleans;

    public interface IPlayer : IGrainObserver
    {
        void GameChanged(Try<Chessboard> chessboard);

        void YourMove(IMatch match);
    }
}
