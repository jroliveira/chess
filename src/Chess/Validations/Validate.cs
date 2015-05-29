using Chess.Pieces;

namespace Chess.Validations
{
    public abstract class Validate
    {
        protected readonly Piece Piece;
        protected Validate NextValidate;

        protected Validate(Piece piece)
        {
            Piece = piece;
        }

        public void SetNextValidate(Validate nexValidate)
        {
            NextValidate = nexValidate;
        }

        public bool IsValid(Position newPosition)
        {
            if (IsValidRule(newPosition))
            {
                return true;
            }

            if (NextValidate == null)
            {
                return false;
            }

            return NextValidate.IsValid(newPosition);
        }

        protected abstract bool IsValidRule(Position newPosition);
    }
}