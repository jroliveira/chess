namespace Chess.Lib.Validations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.KingValidations;

    internal class KingValidator : IValidator
    {
        private readonly FileAndRankLimitValidate fileAndRankLimitValidate;

        public KingValidator(Piece king)
            : this(new FileAndRankLimitValidate(king))
        {
        }

        internal KingValidator(FileAndRankLimitValidate fileAndRankLimitValidate) => this.fileAndRankLimitValidate = fileAndRankLimitValidate;

        public bool Validate(Position newPosition)
        {
            this.fileAndRankLimitValidate.SetNextValidate(null);

            return this.fileAndRankLimitValidate.IsValid(newPosition);
        }
    }
}