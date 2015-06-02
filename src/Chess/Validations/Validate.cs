using Chess.Pieces;

namespace Chess.Validations
{
    public abstract class Validate
    {
        protected readonly Piece Piece;
        protected Validate NextValidate;

        internal Validate() { }

        protected Validate(Piece piece)
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