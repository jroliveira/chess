namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;
    using Chess.Models;

    internal class Queen : Piece
    {
        public Queen(Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected Queen()
        {
        }

        protected override IValidator Validator => new QueenValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♕", "♛");
    }
}