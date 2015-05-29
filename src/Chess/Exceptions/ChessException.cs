using System;

namespace Chess.Exceptions
{
    public class ChessException : ApplicationException
    {
        public ChessException(string message)
            : base(message)
        { }
    }
}