namespace Chess.Lib.Validations.PawnValidations
{
    using System;
    using Chess.Entities;
    using Chess.Models;

    using Piece = Chess.Entities.Pieces.Piece;

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
                return this.Piece.Chessboard.HasPiece(newPosition);
            }

            return false;
        }

        private int Direction(int value)
        {
            if (this.Piece.Owner.Equals(Owner.SecondPlayer))
            {
                return value;
            }

            return -1 * value;
        }
    }
}