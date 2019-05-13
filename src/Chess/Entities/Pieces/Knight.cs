namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Knight : Piece
    {
        public Knight(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♘", "♞")
        {
        }

        protected Knight()
        {
        }

        protected override IValidator Validator => new KnightValidator(this);
    }
}
