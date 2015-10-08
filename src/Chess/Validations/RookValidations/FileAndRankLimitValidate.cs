using System;
using Chess.Pieces;

namespace Chess.Validations.RookValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate()
        {

        }

        public FileAndRankLimitValidate(Piece rook)
            : base(rook)
        {

        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (fileMoved.Equals(rankMoved))
            {
                return false;
            }

            if (fileMoved > 0 && rankMoved > 0)
            {
                return false;
            }

            return true;
        }
    }
}