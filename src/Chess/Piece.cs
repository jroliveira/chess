namespace Chess
{
    using System;

    public sealed class Piece : IEquatable<Piece>
    {
        private readonly char symbol;

        private Piece(char symbol) => this.symbol = symbol;

        public static implicit operator char(Piece piece) => piece.symbol;

        public static implicit operator Piece(char symbol) => new Piece(symbol);

        public override string ToString() => this.symbol.ToString();

        public bool Equals(Piece? other) => this.symbol.Equals(other?.symbol);
    }
}
