namespace Chess.Domain.Pieces.Pawn
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class PawnValidator : ValidatorBase
    {
        internal PawnValidator()
            : base(new PawnMoveValidate(None()))
        {
        }
    }
}
