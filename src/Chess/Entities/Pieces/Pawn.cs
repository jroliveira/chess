namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Pawn : Piece
    {
        public Pawn(Position position, Player player, Chessboard chessboard)
            : base(position, player, chessboard, "♙", "♟")
        {
        }

        protected Pawn()
        {
        }

        protected override IValidator Validator => new PawnValidator(this);
    }
}
