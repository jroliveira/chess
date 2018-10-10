namespace Chess.Models
{
    public sealed class Piece
    {
        private readonly string symbol;

        public Piece(string symbol) => this.symbol = symbol;

        public static implicit operator string(Piece piece) => piece.symbol;

        public static implicit operator Piece(string symbol) => new Piece(symbol);

        public override string ToString() => this.symbol;
    }
}
