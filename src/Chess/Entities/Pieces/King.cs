namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class King : Piece
    {
        public King(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♔", "♚")
        {
        }

        protected King()
        {
        }

        protected override IValidator Validator => new KingValidator(this);
    }
}
