namespace Chess.Lib.Validations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.BishopValidations;

    internal class BishopValidator : IValidator
    {
        private readonly FileAndRankLimitValidate fileAndRankLimitValidate;

        public BishopValidator(Piece bishop)
            : this(new FileAndRankLimitValidate(bishop))
        {
        }

        internal BishopValidator(FileAndRankLimitValidate fileAndRankLimitValidate) => this.fileAndRankLimitValidate = fileAndRankLimitValidate;

        public bool Validate(Position newPosition)
        {
            this.fileAndRankLimitValidate.SetNextValidate(null);

            return this.fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}