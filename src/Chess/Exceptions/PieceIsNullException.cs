namespace Chess.Exceptions
{
    public class PieceIsNullException : ChessException
    {
        public PieceIsNullException(string piece)
            : base("( {0} is an invalid piece! )", piece)
        { }
    }
}