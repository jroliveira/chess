namespace Chess.Domain.Pieces.Knight
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class KnightValidator : ValidatorBase
    {
        internal KnightValidator()
            : base(new KnightMoveValidate(None()))
        {
        }
    }
}
