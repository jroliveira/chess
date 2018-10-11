namespace Chess.Lib.Monad
{
    using System;

    using Chess.Lib.Extensions;

    using static Chess.Lib.Monad.Utils.Util;

    public readonly struct Try<TSuccess>
    {
        private readonly Exception failure;
        private readonly TSuccess success;

        internal Try(Exception failure)
        {
            this.IsFailure = true;
            this.failure = failure;
            this.success = default;
        }

        internal Try(TSuccess success)
        {
            this.IsFailure = false;
            this.failure = default;
            this.success = success;
        }

        public bool IsFailure { get; }

        public static implicit operator Try<TSuccess>(Exception failure) => Failure<TSuccess>(failure);

        public static implicit operator Try<TSuccess>(TSuccess success) => Success(success);

        public Unit Match(Action<Exception> failure, Action<TSuccess> success) => Match(failure.ToFunc(), success.ToFunc());

        public TReturn Match<TReturn>(Func<Exception, TReturn> failure, Func<TSuccess, TReturn> success) => this.IsFailure
            ? failure(this.failure)
            : success(this.success);

        public Try<TReturn> Map<TReturn>(Func<TSuccess, TReturn> mapper) => !this.IsFailure
            ? mapper(this.success)
            : Failure<TReturn>(this.failure);
    }
}
