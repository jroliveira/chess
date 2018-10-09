namespace Chess.Lib.Monad.Utils
{
    using System;

    public static partial class Util
    {
        public static Try<TSuccess> Success<TSuccess>(TSuccess success) => new Try<TSuccess>(success);

        public static Try<TValue> Failure<TValue>(Exception exception) => new Try<TValue>(exception);
    }
}
