namespace Chess.Lib.Validations.KnightValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Math;

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
            var fileMoved = Abs(this.Piece.Position.GetFileMoves(newPosition.File));
            var rankMoved = Abs(this.Piece.Position.GetRankMoves(newPosition.Rank));

            if (fileMoved == 1 && rankMoved == 2)
            {
                return true;
            }

            if (fileMoved == 2 && rankMoved == 1)
            {
                return true;
            }

            return false;
        }
    }
}
