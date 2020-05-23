namespace Chess.Domain.Pieces.Shared
{
    using System.Linq;

    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal abstract class ValidateBase
    {
        private readonly Option<ValidateBase> nextValidateOption;

        protected ValidateBase(Option<ValidateBase> nextValidateOption) => this.nextValidateOption = nextValidateOption;

        public bool IsValid(
            Option<PieceBase> pieceOption,
            Option<Position> newPositionOption,
            Chessboard chessboard) => pieceOption.Fold(false)(piece => newPositionOption
                .Fold(false)(newPosition =>
                {
                    if (!this.IsValidRule(piece, newPosition, chessboard))
                    {
                        return false;
                    }

                    return this.nextValidateOption
                        .Fold(true)(nextValidate => nextValidate.IsValid(piece, newPositionOption, chessboard));
                }));

        protected abstract bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard);
    }
}
