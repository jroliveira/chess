namespace Chess.Domain.Pieces.Shared
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal abstract class ValidateBase
    {
        private readonly Option<ValidateBase> nextValidateOption;

        protected ValidateBase(Option<ValidateBase> nextValidateOption) => this.nextValidateOption = nextValidateOption;

        public bool IsValid(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard)
        {
            if (!this.IsValidRule(piece, newPosition, chessboard))
            {
                return false;
            }

            return this.nextValidateOption.Match(
                nextValidate => nextValidate.IsValid(piece, newPosition, chessboard),
                () => true);
        }

        protected abstract bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard);
    }
}
