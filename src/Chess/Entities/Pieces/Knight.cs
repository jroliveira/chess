namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Knight : Piece
    {
        public Knight(Position position, Chessboard chessboard)
            : base(position, chessboard, "♘", "♞")
        {
        }

        protected Knight()
        {
        }

        protected override IValidator Validator => new KnightValidator(this);
    }
}
