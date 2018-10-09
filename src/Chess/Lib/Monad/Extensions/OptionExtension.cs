namespace Chess.Lib.Monad.Extensions
{
    public static class OptionExtension
    {
        public static TValue GetOrElse<TValue>(in this Option<TValue> @this, TValue @default) => @this.Match(
            value => value,
            () => @default);
    }
}
