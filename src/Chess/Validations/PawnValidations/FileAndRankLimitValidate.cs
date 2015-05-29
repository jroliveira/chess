﻿using System;
using Chess.Pieces;

namespace Chess.Validations.PawnValidations
{
    public class FileAndRankLimitValidate : Validate
    {
        public FileAndRankLimitValidate(Pawn pawn)
            : base(pawn)
        { }

        protected override bool IsValidRule(Position newPosition)
        {
            var fileMoved = Piece.Position.File - newPosition.File;
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