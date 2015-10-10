namespace Chess.Validations
{
    using Chess.Pieces;
    using Chess.Validations.RookValidations;

    internal class RookValidator : IValidator
    {
        private readonly FileAndRankLimitValidate fileAndRankLimitValidate;

        public RookValidator(Piece rook)
            : this(new FileAndRankLimitValidate(rook))
        {
        }

        internal RookValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
        {
            this.fileAndRankLimitValidate = fileAndRankLimitValidate;
        }

        public bool Validate(Position newPosition)
        {
            this.fileAndRankLimitValidate.SetNextValidate(null);

            return this.fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}