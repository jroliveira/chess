using System;
using Chess.Pieces;

namespace Chess.Validations.KnightValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate()
        {

        }

        public FileAndRankLimitValidate(Piece knight)
            : base(knight)
        {

        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (fileMoved.Equals(1) && rankMoved.Equals(2))
            {
                return true;
            }

            if (fileMoved.Equals(2) && rankMoved.Equals(1))
            {
                return true;
            }

            return false;
        }
    }
}