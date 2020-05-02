namespace Chess.Domain.Pieces.King
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class KingValidator : ValidatorBase
    {
        internal KingValidator()
            : base(new KingMoveValidate(None()))
        {
        }
    }
}
