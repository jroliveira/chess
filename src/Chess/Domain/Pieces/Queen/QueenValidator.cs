namespace Chess.Domain.Pieces.Queen
{
    using Chess.Domain.Pieces.Shared;

    using static Chess.Infra.Monad.Utils.Util;

    internal sealed class QueenValidator : ValidatorBase
    {
        internal QueenValidator()
            : base(new QueenMoveValidate(None()))
        {
        }
    }
}
