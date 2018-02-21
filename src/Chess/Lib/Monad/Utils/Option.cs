namespace Chess.Lib.Monad.Utils
{
    public static partial class Util
    {
        public static readonly NoneType None = default;

        public static Option<TValue> Some<TValue>(TValue value) => new Option<TValue>(value, value != null);
    }
}
