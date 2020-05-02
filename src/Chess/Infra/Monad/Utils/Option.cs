namespace Chess.Infra.Monad.Utils
{
    public static partial class Util
    {
        public static None None() => default;

        public static Option<TValue> Some<TValue>(TValue value) => new Option<TValue>(value, value != null);
    }
}
