﻿namespace Chess.Domain.Pieces.Rook
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Pieces.Shared;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class RookMoveValidate : ValidateBase
    {
        public RookMoveValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            var (filesToMove, ranksToMove) = piece.GetDistanceTo(newPosition);

            if (filesToMove > 0 && ranksToMove > 0)
            {
                return false;
            }

            return true;
        }
    }
}
