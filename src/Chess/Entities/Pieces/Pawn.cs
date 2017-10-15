namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;
    using Chess.Models;

    internal class Pawn : Piece
    {
        public Pawn(Owner owner, Position position, Chessboard chessboard)
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