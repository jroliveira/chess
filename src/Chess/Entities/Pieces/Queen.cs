namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Queen : Piece
    {
        public Queen(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♕", "♛")
        {
        }

        protected Queen()
        {
        }

        protected override IValidator Validator => new QueenValidator(this);
    }
}
