namespace Chess.Lib.Validations.KingValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Math;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece king)
            : base(king)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Abs(this.Piece.Position.GetFileMoves(newPosition.File));
            var rankMoved = Abs(this.Piece.Position.GetRankMoves(newPosition.Rank));

            if (fileMoved == 0 && rankMoved == 0)
            {
                return false;
            }

            if (fileMoved > 1)
            {
                return false;
            }

            if (rankMoved > 1)
            {
                return false;
            }

            return true;
        }
    }
}
