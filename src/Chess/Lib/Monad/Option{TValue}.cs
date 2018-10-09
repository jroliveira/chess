namespace Chess.Lib.Monad
{
    using System;
    using Chess.Lib.Monad.Extensions;
    using static Chess.Lib.Monad.Utils.Util;

    public readonly struct Option<TValue>
    {
        public static readonly Option<TValue> None = default;

        private readonly TValue value;

        internal Option(TValue value, bool isDefined)
        {
            this.value = value;
            this.IsDefined = isDefined;
        }

        public bool IsDefined { get; }

        public static implicit operator Option<TValue>(TValue value) => Some(value);

        public static implicit operator Option<TValue>(NoneType none) => None;

        public static implicit operator Option<TValue>(Try<TValue> @try) => @try.ToOption();

        public TValue Get() => this.value;

        public TReturn Match<TReturn>(Func<TValue, TReturn> some, Func<TReturn> none) => this.IsDefined
            ? some(this.value)
            : none();
    }
}
