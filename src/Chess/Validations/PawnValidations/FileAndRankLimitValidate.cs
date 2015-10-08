using System;
using Chess.Pieces;

namespace Chess.Validations.PawnValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate()
        {

        }

        public FileAndRankLimitValidate(Piece pawn)
            : base(pawn)
        {

        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Piece.Position.Rank - newPosition.Rank;

            if (fileMoved > 1)
            {
                return false;
            }

            if (fileMoved.Equals(0))
            {
                if (rankMoved.Equals(Direction(1)) || rankMoved.Equals(Direction(2)))
                {
                    return true;
                }

                return false;
            }

            if (fileMoved.Equals(1) && rankMoved.Equals(Direction(1)))
            {
                var hasPiece = Piece.Chessboard.HasPiece(newPosition);

                return hasPiece;
            }

            return false;
        }

        public int Direction(int value)
        {
            if (Piece.Player.Equals(2))
            {
                return value;
            }

            return -1 * value;
        }
    }
}