namespace Chess.Validations
{
    using Chess.Pieces;
    using Chess.Validations.PawnValidations;

    internal class PawnValidator : IValidator
    {
        private readonly FileAndRankLimitValidate fileAndRankLimitValidate;

        public PawnValidator(Piece pawn)
            : this(new FileAndRankLimitValidate(pawn))
        {
        }

        internal PawnValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
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