namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;
    using Chess.Models;

    internal class Rook : Piece
    {
        public Rook(Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected Rook()
        {
        }

        protected override IValidator Validator => new RookValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♖", "♜");
    }
}