namespace Chess.Lib.Validations.RookValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Math;

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
            var fileMoved = Abs(this.Piece.Position.GetFileMoves(newPosition.File));
            var rankMoved = Abs(this.Piece.Position.GetRankMoves(newPosition.Rank));

            if (fileMoved == rankMoved)
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
