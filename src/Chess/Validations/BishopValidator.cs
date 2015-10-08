using Chess.Pieces;
using Chess.Validations.BishopValidations;

namespace Chess.Validations
{
    internal class BishopValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal BishopValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public BishopValidator(Piece bishop)
            : this(new FileAndRankLimitValidate(bishop))
        {
            
        }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}