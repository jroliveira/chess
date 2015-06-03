using Chess.Game.Pieces;
using Chess.Game.Validations.PawnValidations;

namespace Chess.Game.Validations
{
    internal class PawnValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;
        private readonly HasPieceValidate _hasPieceValidate;

        internal PawnValidator(FileAndRankLimitValidate fileAndRankLimitValidate,
                               HasPieceValidate hasPieceValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
            _hasPieceValidate = hasPieceValidate;
        }

        public PawnValidator(Pawn pawn)
            : this(new FileAndRankLimitValidate(pawn), new HasPieceValidate(pawn))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(_hasPieceValidate);
            _hasPieceValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}