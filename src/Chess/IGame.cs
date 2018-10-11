namespace Chess
{
    using Chess.Lib.Monad;
    using Chess.Models;

    public interface IGame
    {
        Try<Chessboard> Start();

        Try<Player> JoinPlayer(Option<string> playerName);

        Try<Chessboard> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName);
    }
}
