using Chess.Pieces;
using Chess.Validations.KingValidations;

namespace Chess.Validations
{
    internal class KingValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal KingValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public KingValidator(Piece king)
            : this(new FileAndRankLimitValidate(king))
        {
            
        }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}