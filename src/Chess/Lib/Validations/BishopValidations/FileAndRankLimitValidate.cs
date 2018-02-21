namespace Chess.Lib.Validations.BishopValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Math;

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
            var fileMoved = Abs(this.Piece.Position.GetFileMoves(newPosition.File));
            var rankMoved = Abs(this.Piece.Position.GetRankMoves(newPosition.Rank));

            if (fileMoved == 0 || rankMoved == 0)
            {
                return false;
            }

            if (fileMoved == rankMoved)
            {
                return true;
            }

            return false;
        }
    }
}
