namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;
    using Chess.Models;

    internal class King : Piece
    {
        public King(Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected King()
        {
        }

        protected override IValidator Validator => new KingValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♔", "♚");
    }
}