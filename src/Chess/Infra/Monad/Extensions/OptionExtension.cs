namespace Chess.Infra.Monad.Extensions
{
    using System.Linq;

    public static class OptionExtension
    {
        public static TValue GetOrElse<TValue>(this Option<TValue> @this, TValue @default) => @this
            .Fold(@default)(_ => _);
    }
}
