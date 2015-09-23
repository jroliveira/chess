using Chess.Validations;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        private readonly IValidator _validator;

        protected override IValidator Validator { get { return _validator; } }

        protected Bishop()
        {

        }

        internal Bishop(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            _validator = validator;
        }

        public Bishop(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            _validator = new BishopValidator(this);
        }
    }
}