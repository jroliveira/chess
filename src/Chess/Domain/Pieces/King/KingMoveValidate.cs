namespace Chess.Domain.Pieces.King
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class KingMoveValidate : ValidateBase
    {
        public KingMoveValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            if (filesToMove > 1)
            {
                return false;
            }

            if (ranksToMove > 1)
            {
                return false;
            }

            return true;
        }
    }
}
