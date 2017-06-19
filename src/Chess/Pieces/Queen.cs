namespace Chess.Pieces
{
    using Chess.Validations;

    internal class Queen : Piece
    {
        public Queen(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new QueenValidator(this);
        }

        internal Queen(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected Queen()
        {
        }

        public override string Name => this.Player == 1 ? "♕" : "♛";

        protected override IValidator Validator { get; }
    }
}