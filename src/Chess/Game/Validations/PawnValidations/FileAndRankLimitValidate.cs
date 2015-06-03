using System;
using Chess.Game.Pieces;

namespace Chess.Game.Validations.PawnValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate() { }

        public FileAndRankLimitValidate(Pawn pawn)
            : base(pawn)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (fileMoved > 1)
            {
                return false;
            }

            if (rankMoved < 1 || rankMoved > 2)
            {
                return false;
            }

            if (fileMoved.Equals(1) && !rankMoved.Equals(1))
            {
                return false;
            }

            return true;
        }
    }
}