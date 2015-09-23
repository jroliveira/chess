using System;

namespace Chess.Exceptions
{
    public class ChessException : ApplicationException
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