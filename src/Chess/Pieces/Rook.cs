using Chess.Validations;

namespace Chess.Pieces
{
    internal class Rook : Piece
    {
        private readonly IValidator _validator;

        protected override IValidator Validator { get { return _validator; } }

        protected Rook()
        {

        }

        internal Rook(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            _validator = validator;
        }

        public Rook(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            _validator = new RookValidator(this);
        }
    }
}