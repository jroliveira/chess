namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Pawn : Piece
    {
        public Pawn(Position position, Chessboard chessboard)
            : base(position, chessboard, "♙", "♟")
        {
        }

        protected Pawn()
        {
        }

        protected override IValidator Validator => new PawnValidator(this);
    }
}
