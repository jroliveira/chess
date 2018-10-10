namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Queen : Piece
    {
        public Queen(Position position, Chessboard chessboard)
            : base(position, chessboard, "♕", "♛")
        {
        }

        protected Queen()
        {
        }

        protected override IValidator Validator => new QueenValidator(this);
    }
}
