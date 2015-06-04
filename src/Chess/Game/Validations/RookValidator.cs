using Chess.Game.Pieces;
using Chess.Game.Validations.RookValidations;

namespace Chess.Game.Validations
{
    internal class RookValidator : IValidator
    {
        private readonly FileAndRankLimitValidate _fileAndRankLimitValidate;

        internal RookValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            _fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public RookValidator(Rook rook)
            : this(new FileAndRankLimitValidate(rook))
        { }

        public bool Validate(Position newPosition)
        {
            _fileAndRankLimitValidate.SetNextValidate(null);

            return _fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}