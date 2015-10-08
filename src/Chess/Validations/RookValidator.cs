using Chess.Pieces;
using Chess.Validations.RookValidations;

namespace Chess.Validations
{
    internal class RookValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal RookValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public RookValidator(Piece rook)
            : this(new FileAndRankLimitValidate(rook))
        {
            
        }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}