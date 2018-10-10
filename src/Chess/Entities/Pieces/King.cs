namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class King : Piece
    {
        public King(Position position, Chessboard chessboard)
            : base(position, chessboard, "♔", "♚")
        {
        }

        protected King()
        {
        }

        protected override IValidator Validator => new KingValidator(this);
    }
}
