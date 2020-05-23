namespace Chess.Domain.Pieces.Shared
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal abstract class ValidatorBase
    {
        private readonly ValidateBase validate;

        protected ValidatorBase(ValidateBase validate) => this.validate = new MovedValidate(new FriendlyFireValidate(validate));

        public bool Validate(Option<PieceBase> piece, Option<Position> newPositionOption, Chessboard chessboard) => this.validate.IsValid(piece, newPositionOption, chessboard);
    }
}
