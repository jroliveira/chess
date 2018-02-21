namespace Chess.Lib.Validations.KnightValidations
{
    using System;
    using Chess.Entities;
    using Chess.Entities.Pieces;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece knight)
            : base(knight)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(this.Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(this.Piece.Position.Rank - newPosition.Rank);

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