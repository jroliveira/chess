namespace Chess.Lib.Validations.BishopValidations
{
    using System;

    using Chess.Entities;
    using Chess.Entities.Pieces;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece bishop)
            : base(bishop)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(this.Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(this.Piece.Position.Rank - newPosition.Rank);

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