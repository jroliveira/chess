namespace Chess.Infra.Monad.Utils
{
    public static partial class Util
    {
        public static Try<TSuccess> Success<TSuccess>(TSuccess success) => new Try<TSuccess>(success);

        public static Try<TValue> Failure<TValue>(ChessException exception) => new Try<TValue>(exception);
    }
}
