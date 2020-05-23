namespace Chess.Infra.Monad.Extensions
{
    using static Chess.Infra.Monad.Utils.Util;

    public static class TryExtension
    {
        public static TValue GetOrElse<TValue>(this Try<TValue> @this, TValue @default) => @this.Match(
            _ => @default,
            value => value);

        public static Option<TSuccess> ToOption<TSuccess>(this Try<TSuccess> @this) => @this.Match(
            _ => None(),
            Some);
    }
}
