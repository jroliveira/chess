using System;
using Chess.Game.Pieces;

namespace Chess.Game.Validations.PawnValidations
{
    internal class HasPieceValidate : Validate
    {
        protected HasPieceValidate() { }

        public HasPieceValidate(Pawn pawn)
            : base(pawn)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var hasPiece = Piece.Chessboard.HasPiece(newPosition);

            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Piece.Position.Rank - newPosition.Rank;

            if (fileMoved.Equals(1) && rankMoved.Equals(1))
            {
                return hasPiece;
            }

            return true;
        }
    }
}