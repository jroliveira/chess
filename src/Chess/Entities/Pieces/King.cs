namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class King : Piece
    {
        public King(Models.Owner owner, Position position, Chessboard chessboard)
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