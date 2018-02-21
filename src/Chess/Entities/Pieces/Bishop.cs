namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Bishop : Piece
    {
        public Bishop(Position position, Chessboard chessboard)
            : base(position, chessboard, "♗", "♝")
        {
        }

        protected Bishop()
        {
        }

        protected override IValidator Validator => new BishopValidator(this);
    }
}
