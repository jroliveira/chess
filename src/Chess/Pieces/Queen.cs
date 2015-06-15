using Chess.Validations;

namespace Chess.Pieces
{
    internal class Queen : Piece
    {
        private readonly IValidator _validator;

        protected override IValidator Validator { get { return _validator; } }

        protected Queen()
        {

        }

        internal Queen(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            _validator = validator;
        }

        public Queen(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            _validator = new QueenValidator(this);
        }
    }
}