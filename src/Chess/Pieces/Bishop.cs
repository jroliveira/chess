namespace Chess.Pieces
{
    using Chess.Validations;

    internal class Bishop : Piece
    {
        public Bishop(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new BishopValidator(this);
        }

        internal Bishop(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected Bishop()
        {
        }

        public override string Name => this.Player == 1 ? "♗" : "♝";

        protected override IValidator Validator { get; }
    }
}