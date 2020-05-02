namespace Chess.Domain.Pieces.Shared
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;

    internal abstract class ValidatorBase
    {
        private readonly ValidateBase validate;

        protected ValidatorBase(ValidateBase validate) => this.validate = new MovedValidate(new FriendlyFireValidate(validate));

        public bool Validate(PieceBase piece, Position newPosition, Chessboard chessboard) => this.validate.IsValid(piece, newPosition, chessboard);
    }
}
