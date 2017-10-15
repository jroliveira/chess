namespace Chess.Lib.Validations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;
    using Chess.Lib.Validations.KnightValidations;

    internal class KnightValidator : IValidator
    {
        private readonly FileAndRankLimitValidate fileAndRankLimitValidate;

        public KnightValidator(Piece knight)
            : this(new FileAndRankLimitValidate(knight))
        {
        }

        internal KnightValidator(FileAndRankLimitValidate fileAndRankLimitValidate)
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