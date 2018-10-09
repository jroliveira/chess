namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Bishop : Piece
    {
        public Bishop(Models.Owner owner, Position position, Chessboard chessboard)
            : base(owner, position, chessboard)
        {
        }

        protected Bishop()
        {
        }

        protected override IValidator Validator => new BishopValidator(this);

        protected override (string FirstPlayer, string SecondPlayer) Symbols => ("♗", "♝");
    }
}