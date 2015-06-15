namespace Chess.Validations
{
    internal abstract class Validate
    {
        protected readonly Pieces.Piece Piece;
        protected Validate NextValidate;

        internal Validate() { }

        protected Validate(Pieces.Piece piece)
        {
            Piece = piece;
        }

        public virtual void SetNextValidate(Validate nexValidate)
        {
            NextValidate = nexValidate;
        }

        public virtual bool IsValid(Position newPosition)
        {
            if (!IsValidRule(newPosition))
            {
                return false;
            }

            if (NextValidate == null)
            {
                return true;
            }

            return NextValidate.IsValid(newPosition);
        }

        protected abstract bool IsValidRule(Position newPosition);
    }
}