namespace Chess.Entities.Pieces
{
    using Chess.Lib.Validations;

    internal class Rook : Piece
    {
        public Rook(Models.Owner owner, Position position, Chessboard chessboard)
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