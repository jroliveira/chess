using Chess.Game.Pieces;
using Chess.Game.Validations.BishopValidations;

namespace Chess.Game.Validations
{
    internal class BishopValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal BishopValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public BishopValidator(Bishop bishop)
            : this(new FileAndRankLimitValidate(bishop))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}