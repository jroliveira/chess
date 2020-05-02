namespace Chess.Domain.Pieces.Shared
{
    using Chess.Domain.Chessboard;
    using Chess.Domain.Shared;
    using Chess.Infra.Monad;

    internal sealed class FriendlyFireValidate : ValidateBase
    {
        public FriendlyFireValidate(Option<ValidateBase> nextValidateOption)
            : base(nextValidateOption)
        {
        }

        protected override bool IsValidRule(
            PieceBase piece,
            Position newPosition,
            Chessboard chessboard) => chessboard.GetPiece(newPosition).Match(
                otherPiece => !piece.SameColor(otherPiece),
                () => true);
    }
}
