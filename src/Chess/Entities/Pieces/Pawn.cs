namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Pawn : Piece
    {
        public Pawn(Models.Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected Pawn()
        {
        }

        protected override IValidator Validator => new PawnValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♙", "♟");
    }
}