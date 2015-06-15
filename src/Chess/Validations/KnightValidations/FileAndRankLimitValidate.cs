using System;
using Chess.Pieces;

namespace Chess.Validations.KnightValidations
{
    internal class FileAndRankLimitValidate : Validate
    {
        protected FileAndRankLimitValidate() { }

        public FileAndRankLimitValidate(Knight knight)
            : base(knight)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Math.Abs(Piece.Position.File - newPosition.File);
            var rankMoved = Math.Abs(Piece.Position.Rank - newPosition.Rank);

           //TODO: Role

            return true;
        }
    }
}