using System;
using Chess.Pieces;

namespace Chess.Validations.PawnValidations
{
    public class HasPieceValidate : Validate
    {
        public HasPieceValidate(Pawn pawn)
            : base(pawn)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var hasPiece = Piece.Chessboard.HasPiece(newPosition.ToString());

            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (hasPiece && rankMoved.Equals(2))
            {
                return false;
            }

            return true;
        }
    }
}