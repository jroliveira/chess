namespace Chess.Entities
{
    using System;

    using static System.Convert;

    internal class Position : IEquatable<Position>
    {
        internal Position(char file, uint rank)
        {
            this.File = file;
            this.Rank = rank;
        }

        protected Position()
        {
        }

        internal virtual char File { get; }

        internal virtual uint Rank { get; }

        public virtual bool Equals(Position other) => this.File == other.File && this.Rank == other.Rank;

        public override string ToString() => $"{this.File}{this.Rank}";

        internal int GetFileMoves(char file) => this.File - file;

        internal int GetRankMoves(uint rank) => ToInt32(this.Rank) - ToInt32(rank);
    }
}
