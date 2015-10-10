namespace Chess.Validations
{
    using Chess.Pieces;

    using Bishop = Chess.Validations.BishopValidations;
    using Rook = Chess.Validations.RookValidations;

    internal class QueenValidator : IValidator
    {
        private readonly Bishop.FileAndRankLimitValidate bishopFileAndRankLimitValidate;
        private readonly Rook.FileAndRankLimitValidate rookFileAndRankLimitValidate;

        public QueenValidator(Piece queen)
            : this(new Bishop.FileAndRankLimitValidate(queen), new Rook.FileAndRankLimitValidate(queen))
        {
        }

        internal QueenValidator(
            Bishop.FileAndRankLimitValidate fileAndRankLimitValidate,
            Rook.FileAndRankLimitValidate rookFileAndRankLimitValidate)
        {
            this.bishopFileAndRankLimitValidate = fileAndRankLimitValidate;
            this.rookFileAndRankLimitValidate = rookFileAndRankLimitValidate;
        }

        public bool Validate(Position newPosition)
        {
            this.bishopFileAndRankLimitValidate.SetNextValidate(this.rookFileAndRankLimitValidate);
            this.rookFileAndRankLimitValidate.SetNextValidate(null);

            return this.bishopFileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}