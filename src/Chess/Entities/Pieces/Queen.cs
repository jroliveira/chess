namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Queen : Piece
    {
        public Queen(Models.Owner owner, Position position, Chessboard chessboard)
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