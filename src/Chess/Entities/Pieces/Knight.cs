namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Knight : Piece
    {
        public Knight(Models.Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected Knight()
        {
        }

        protected override IValidator Validator => new KnightValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♘", "♞");
    }
}