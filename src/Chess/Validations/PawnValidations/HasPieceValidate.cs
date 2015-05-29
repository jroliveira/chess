using System;
using Chess.Pieces;

namespace Chess.Validations.PawnValidations
{
    public class HasPieceValidate : Validate
    {
        protected HasPieceValidate() { }

        public HasPieceValidate(Piece piece)
            : base(piece) 
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var hasPiece = Piece.Chessboard.HasPiece(newPosition.ToString());

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