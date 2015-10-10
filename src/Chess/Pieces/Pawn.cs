using Chess.Validations;

namespace Chess.Pieces
{
    internal class Pawn : Piece
    {
        private readonly IValidator _validator;

        public override string Name
        {
            get { return Player == 1 ? "♙" : "♟"; }
        }

        protected override IValidator Validator { get { return _validator; } }

        protected Pawn()
        {

        }

        internal Pawn(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            _validator = validator;
        }

        public Pawn(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            _validator = new PawnValidator(this);
        }
    }
}