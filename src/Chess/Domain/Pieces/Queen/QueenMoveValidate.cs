namespace Chess.Domain.Pieces.Queen
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class QueenMoveValidate : ValidateBase
    {
        public QueenMoveValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            if (filesToMove == ranksToMove)
            {
                return true;
            }

            if (filesToMove == 0 || ranksToMove == 0)
            {
                return true;
            }

            return false;
        }
    }
}
