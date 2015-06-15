using Chess.Pieces;

namespace Chess.Validations
{
    internal abstract class Validate
    {
        private Validate _nextValidate;

        protected readonly Piece Piece;

        internal Validate()
        {

        }

        protected Validate(Piece piece)
        {
            Piece = piece;
        }

        public virtual void SetNextValidate(Validate nexValidate)
        {
            _nextValidate = nexValidate;
        }

        public virtual bool IsValid(Position newPosition)
        {
            if (IsValidRule(newPosition))
            {
                return true;
            }

            if (_nextValidate == null)
            {
                return false;
            }

            return _nextValidate.IsValid(newPosition);
        }

        protected abstract bool IsValidRule(Position newPosition);
    }
}