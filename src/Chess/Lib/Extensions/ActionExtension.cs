namespace Chess.Lib.Extensions
{
    using System;

    using Chess.Lib.Monad;

    using static Chess.Lib.Monad.Utils.Util;

    internal static class ActionExtension
    {
        internal static Func<TParam, Unit> ToFunc<TParam>(this Action<TParam> @this) => param =>
        {
            @this(param);

            return Unit();
        };

        internal static Func<Unit> ToFunc(this Action @this) => () =>
        {
            @this();

            return Unit();
        };
    }
}
