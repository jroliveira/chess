namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Rook : Piece
    {
        public Rook(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♖", "♜")
        {
        }

        protected Rook()
        {
        }

        protected override IValidator Validator => new RookValidator(this);
    }
}
