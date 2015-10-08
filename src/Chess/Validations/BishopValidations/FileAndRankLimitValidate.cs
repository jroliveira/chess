using System;
using Chess.Pieces;

namespace Chess.Validations.BishopValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate()
        {

        }

        public FileAndRankLimitValidate(Piece bishop)
            : base(bishop)
        {

        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (fileMoved.Equals(0) || rankMoved.Equals(0))
            {
                return false;
            }

            if (fileMoved.Equals(rankMoved))
            {
                return true;
            }

            return false;
        }
    }
}