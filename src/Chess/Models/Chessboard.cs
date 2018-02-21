namespace Chess.Models
{
    using System.Collections.Generic;

    public sealed class Chessboard
    {
        private readonly Piece[,] pieces;

        public Chessboard(
            IReadOnlyCollection<char> files,
            IReadOnlyCollection<uint> ranks,
            Piece[,] pieces)
        {
            this.pieces = pieces;
            this.Files = files;
            this.Ranks = ranks;
        }

        public IReadOnlyCollection<char> Files { get; }

        public IReadOnlyCollection<uint> Ranks { get; }

        public Piece this[int file, int rank] => this.pieces[file, rank];
    }
}
