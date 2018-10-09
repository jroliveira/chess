namespace Chess.Lib.Validations
{
    using Chess.Entities;
    using Chess.Entities.Pieces;

    internal abstract class Validate
    {
        protected readonly Piece Piece;
        private Validate nextValidate;

        internal Validate()
        {
        }

        protected Validate(Piece piece) => this.Piece = piece;

        public virtual void SetNextValidate(Validate nexValidate) => this.nextValidate = nexValidate;

        public virtual bool IsValid(Position newPosition)
        {
            if (this.IsValidRule(newPosition))
            {
                return true;
            }

            if (this.nextValidate == null)
            {
                return false;
            }

            return this.nextValidate.IsValid(newPosition);
        }

        protected abstract bool IsValidRule(Position newPosition);
    }
}