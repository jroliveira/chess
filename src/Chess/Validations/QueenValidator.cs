using Chess.Pieces;
using Bishop = Chess.Validations.BishopValidations;
using Rook = Chess.Validations.RookValidations;

namespace Chess.Validations
{
    internal class QueenValidator : IValidator
    {
        private readonly Bishop.FileAndRankLimitValidate _bishopFileAndRankLimitValidate;
        private readonly Rook.FileAndRankLimitValidate _rookFileAndRankLimitValidate;

        internal QueenValidator(Bishop.FileAndRankLimitValidate fileAndRankLimitValidate,
                                Rook.FileAndRankLimitValidate rookFileAndRankLimitValidate)
        {
            _bishopFileAndRankLimitValidate = fileAndRankLimitValidate;
            _rookFileAndRankLimitValidate = rookFileAndRankLimitValidate;
        }

        public QueenValidator(Piece queen)
            : this(new Bishop.FileAndRankLimitValidate(queen), new Rook.FileAndRankLimitValidate(queen))
        {

        }

        public bool Validate(Position newPosition)
        {
            _bishopFileAndRankLimitValidate.SetNextValidate(_rookFileAndRankLimitValidate);
            _rookFileAndRankLimitValidate.SetNextValidate(null);

            return _bishopFileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}