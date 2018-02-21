namespace Chess.Lib.Validations.PawnValidations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    using static System.Math;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece pawn)
            : base(pawn)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Abs(this.Piece.Position.GetFileMoves(newPosition.File));
            var rankMoved = this.Piece.Position.GetRankMoves(newPosition.Rank);

            if (fileMoved > 1)
            {
                return false;
            }

            if (fileMoved == 0)
            {
                if (rankMoved == this.Direction(1) || rankMoved == this.Direction(2))
                {
                    return true;
                }

                return false;
            }

            if (fileMoved == 1 && rankMoved == this.Direction(1))
            {
                return this.Piece.Chessboard.HasPiece(newPosition);
            }

            return false;
        }

        private int Direction(int value)
        {
            if (!this.Piece.IsWhite)
            {
                return value;
            }

            return -1 * value;
        }
    }
}
