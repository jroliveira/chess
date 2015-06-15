using Chess.Pieces;
using Chess.Validations.KnightValidations;

namespace Chess.Validations
{
    internal class KnightValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal KnightValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public KnightValidator(Knight knight)
            : this(new FileAndRankLimitValidate(knight))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}