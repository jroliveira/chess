namespace Chess.Domain.Pieces.Bishop
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class BishopValidator : ValidatorBase
    {
        internal BishopValidator()
            : base(new BishopMoveValidate(None()))
        {
        }
    }
}
