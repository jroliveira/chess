namespace Chess.Lib.Validations.RookValidations
{
    using System;

    using Chess.Entities;
    using Chess.Entities.Pieces;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece rook)
            : base(rook)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(this.Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(this.Piece.Position.Rank - newPosition.Rank);

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