namespace Chess.Lib.Monad.Extensions
{
    using static Chess.Lib.Monad.Utils.Util;

    public static class TryExtension
    {
        public static TSuccess GetOrElse<TSuccess>(in this Try<TSuccess> @this, TSuccess @default) => @this.Match(
            _ => @default,
            value => value);

        public static Option<TSuccess> ToOption<TSuccess>(in this Try<TSuccess> @this) => @this.Match(
            _ => None,
            Some);
    }
}
