namespace Chess
{
    using Chess.Infra.Monad;

    public interface IGame
    {
        Try<Match> JoinUser(
            Option<string> userNameOption,
            Option<PieceColor> playingWithOption = default);

        Try<Match> MovePiece(
            Option<string> piecePositionOption,
            Option<string> newPositionOption,
            Option<string> userNameOption);
    }
}
