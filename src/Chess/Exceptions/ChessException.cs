namespace Chess.Exceptions
{
    using System;

    public class ChessException : Exception
    {
        public ChessException(string message)
            : base(message)
        {
        }

        public ChessException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}