namespace Chess.Pieces
{
    using Chess.Validations;

    internal class Rook : Piece
    {
        public Rook(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new RookValidator(this);
        }

        internal Rook(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected Rook()
        {
        }

        public override string Name => this.Player == 1 ? "♖" : "♜";

        protected override IValidator Validator { get; }
    }
}