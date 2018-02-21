namespace Chess
{
    using Chess.Lib.Monad;
    using Chess.Models;

    public interface IGame
    {
        Try<Chessboard> Start();

        Try<Chessboard> Move(Option<string> piecePosition, Option<string> newPosition);
    }
}