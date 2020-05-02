namespace Chess.Infra.Monad
{
    using System;
    using System.Runtime.Serialization;

    using static Chess.Infra.Monad.Utils.Util;

    [Serializable]
    public readonly struct Try<TSuccess> : ISerializable
    {
        private readonly ChessException? failure;
        private readonly TSuccess success;

        public Try(ChessException failure)
        {
            this.failure = failure;
            this.success = default;
        }

        public Try(TSuccess success)
        {
            this.failure = default;
            this.success = success;
        }

        public static implicit operator Try<TSuccess>(ChessException failure) => Failure<TSuccess>(failure);

        public static implicit operator Try<TSuccess>(TSuccess success) => Success(success);

        public static implicit operator bool(Try<TSuccess> @try) => @try.ToBoolean();

        public TReturn Match<TReturn>(Func<ChessException, TReturn> failureFunc, Func<TSuccess, TReturn> successFunc) => this.failure != default
            ? failureFunc(this.failure)
            : successFunc(this.success);

        public TSuccess Get() => this.success;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (this.failure != default)
            {
                info.AddValue(nameof(this.failure), this.failure.Message);
            }
            else
            {
                info.AddValue(nameof(this.success), this.success);
            }
        }

        public bool ToBoolean() => this.failure == default;
    }
}
