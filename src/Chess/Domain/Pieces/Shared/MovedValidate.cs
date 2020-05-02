namespace Chess.Domain.Pieces.Shared
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class MovedValidate : ValidateBase
    {
        public MovedValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            return filesToMove != 0 || ranksToMove != 0;
        }
    }
}
