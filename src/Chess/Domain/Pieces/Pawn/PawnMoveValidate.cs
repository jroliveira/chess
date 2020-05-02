namespace Chess.Domain.Pieces.Pawn
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    using static Chess.Domain.Shared.Direction;
    using static Chess.PieceColor;

    internal sealed class PawnMoveValidate : ValidateBase
    {
        public PawnMoveValidate(Option<ValidateBase> nextValidateOption)
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

            if (ranksToMove == 0)
            {
                return false;
            }

            if (ranksToMove > 2)
            {
                return false;
            }

            if (ranksToMove == 2 && !piece.IsFirstMove)
            {
                return false;
            }

            if (chessboard.GetPiece(newPosition) && filesToMove != ranksToMove)
            {
                return false;
            }

            if (filesToMove == ranksToMove && !chessboard.GetPiece(newPosition))
            {
                return false;
            }

            var (horizontal, _) = piece.GetDirectionTo(newPosition);

            if (piece.Color == WhitePiece && horizontal.Equals(Bottom))
            {
                return false;
            }

            if (piece.Color == BlackPiece && horizontal.Equals(Top))
            {
                return false;
            }

            return true;
        }
    }
}
