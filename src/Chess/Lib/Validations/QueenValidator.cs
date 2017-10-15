namespace Chess.Lib.Validations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.BishopValidations;

    internal class QueenValidator : IValidator
    {
        private readonly FileAndRankLimitValidate bishopFileAndRankLimitValidate;
        private readonly RookValidations.FileAndRankLimitValidate rookFileAndRankLimitValidate;

        public QueenValidator(Piece queen)
            : this(new FileAndRankLimitValidate(queen), new RookValidations.FileAndRankLimitValidate(queen))
        {
        }

        internal QueenValidator(
            FileAndRankLimitValidate fileAndRankLimitValidate,
            RookValidations.FileAndRankLimitValidate rookFileAndRankLimitValidate)
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