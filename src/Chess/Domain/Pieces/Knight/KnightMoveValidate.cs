namespace Chess.Domain.Pieces.Knight
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class KnightMoveValidate : ValidateBase
    {
        public KnightMoveValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            if (filesToMove == 1 && ranksToMove == 2)
            {
                return true;
            }

            if (filesToMove == 2 && ranksToMove == 1)
            {
                return true;
            }

            return false;
        }
    }
}
