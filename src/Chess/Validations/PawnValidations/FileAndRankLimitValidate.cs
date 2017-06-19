namespace Chess.Validations.PawnValidations
{
    using System;

    using Chess.Pieces;

    internal class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Piece pawn)
            : base(pawn)
        {
        }

        protected FileAndRankLimitValidate()
        {
        }

        public int Direction(int value)
        {
            if (this.Piece.Player.Equals(2))
            {
                return value;
            }

            return -1 * value;
        }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(this.Piece.Position.File - newPosition.File);
            var rankMoved = this.Piece.Position.Rank - newPosition.Rank;

            if (fileMoved > 1)
            {
                return false;
            }

            if (fileMoved.Equals(0))
            {
                if (rankMoved.Equals(this.Direction(1)) || rankMoved.Equals(this.Direction(2)))
                {
                    return true;
                }

                return false;
            }

            if (fileMoved.Equals(1) && rankMoved.Equals(this.Direction(1)))
            {
                var hasPiece = this.Piece.Chessboard.HasPiece(newPosition);

                return hasPiece;
            }

            return false;
        }
    }
}