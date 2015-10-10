namespace Chess.Pieces
{
    using Chess.Validations;

    internal class Pawn : Piece
    {
        public Pawn(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new PawnValidator(this);
        }

        internal Pawn(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected Pawn()
        {
        }

        public override string Name => this.Player == 1 ? "♙" : "♟";

        protected override IValidator Validator { get; }
    }
}