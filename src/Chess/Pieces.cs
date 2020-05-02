namespace Chess
{
    public sealed class Pieces
    {
        private readonly Piece[,] pieces;

        private Pieces(Piece[,] pieces) => this.pieces = pieces;

        public Piece this[int file, int rank] => this.pieces[file, rank];

        public static implicit operator Piece[,](Pieces pieces) => pieces.pieces;

        public static Pieces CreatePieces(Piece[,] pieces) => new Pieces(pieces);
    }
}
