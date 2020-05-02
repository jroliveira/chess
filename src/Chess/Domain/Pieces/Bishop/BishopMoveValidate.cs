namespace Chess.Domain.Pieces.Bishop
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class BishopMoveValidate : ValidateBase
    {
        public BishopMoveValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            if (filesToMove != ranksToMove)
            {
                return false;
            }

            return true;
        }
    }
}
