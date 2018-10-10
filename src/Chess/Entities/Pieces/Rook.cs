namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Rook : Piece
    {
        public Rook(Position position, Chessboard chessboard)
            : base(position, chessboard, "♖", "♜")
        {
        }

        protected Rook()
        {
        }

        protected override IValidator Validator => new RookValidator(this);
    }
}
