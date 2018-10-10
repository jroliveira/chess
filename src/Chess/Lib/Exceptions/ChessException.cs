namespace Chess.Lib.Exceptions
{
    using System;

    using static System.String;

    public class ChessException : Exception
    {
        public ChessException(string message)
            : base(message)
        {
        }

        public ChessException(string format, params object[] args)
            : base(Format(format, args))
        {
        }
    }
}
