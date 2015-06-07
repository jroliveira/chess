using Chess.Game.Pieces;
using Chess.Game.Validations.KingValidations;

namespace Chess.Game.Validations
{
    internal class KingValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal KingValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public KingValidator(King king)
            : this(new FileAndRankLimitValidate(king))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}