namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Bishop : Piece
    {
        public Bishop(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♗", "♝")
        {
        }

        protected Bishop()
        {
        }

        protected override IValidator Validator => new BishopValidator(this);
    }
}
