namespace Chess.Infra.Monad.Extensions
{
    public static class OptionExtension
    {
        public static TValue GetOrElse<TValue>(this Option<TValue> @this, TValue @default) => @this.Match(
            value => value,
            () => @default);
    }
}
