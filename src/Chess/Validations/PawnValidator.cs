using Chess.Pieces;
using Chess.Validations.PawnValidations;

namespace Chess.Validations
{
    internal class PawnValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal PawnValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public PawnValidator(Piece pawn)
            : this(new FileAndRankLimitValidate(pawn))
        {
            
        }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}