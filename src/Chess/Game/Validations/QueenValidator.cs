using Chess.Game.Pieces;
using Chess.Game.Validations.QueenValidations;

namespace Chess.Game.Validations
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