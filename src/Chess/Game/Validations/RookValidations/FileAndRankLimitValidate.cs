using System;
using Chess.Game.Pieces;

namespace Chess.Game.Validations.RookValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate() { }

        public FileAndRankLimitValidate(Rook rook)
            : base(rook)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

            if (fileMoved > 0 && rankMoved > 0)
            {
                return false;
            }

            return true;
        }
    }
}