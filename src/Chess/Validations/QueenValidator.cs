using Chess.Pieces;
using Chess.Validations.QueenValidations;

namespace Chess.Validations
{
    internal class QueenValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal QueenValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public QueenValidator(Queen queen)
            : this(new FileAndRankLimitValidate(queen))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}