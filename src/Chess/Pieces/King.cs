namespace Chess.Pieces
{
    using Chess.Validations;

    internal class King : Piece
    {
        public King(int player, Position position, Chessboard chessboard)
            : base(player, position, chessboard)
        {
            this.Validator = new KingValidator(this);
        }

        internal King(int player, Position position, Chessboard chessboard, IValidator validator)
            : this(player, position, chessboard)
        {
            this.Validator = validator;
        }

        protected King()
        {
        }

        public override string Name => this.Player == 1 ? "♔" : "♚";

        protected override IValidator Validator { get; }
    }
}