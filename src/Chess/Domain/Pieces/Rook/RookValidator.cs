namespace Chess.Domain.Pieces.Rook
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class RookValidator : ValidatorBase
    {
        internal RookValidator()
            : base(new RookMoveValidate(None()))
        {
        }
    }
}
