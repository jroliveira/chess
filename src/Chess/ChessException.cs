namespace Chess
{
    using System;

    public class ChessException : Exception
    {
        public ChessException()
        {
        }

        public ChessException(string message)
            : base(message)
        {
        }
    }
}
