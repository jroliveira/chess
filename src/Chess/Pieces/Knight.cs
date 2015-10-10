namespace Chess.Pieces
{
    using Chess.Validations;

    internal class Knight : Piece
    {
        public Knight(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new KnightValidator(this);
        }

        internal Knight(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected Knight()
        {
        }

        public override string Name => this.Player == 1 ? "♘" : "♞";

        protected override IValidator Validator { get; }
    }
}