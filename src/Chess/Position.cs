namespace Chess
{
    using System;

    internal class Position : IEquatable<Position>
    {
        public Position(char file, char rank)
        {
            this.File = file;
            this.Rank = rank;
        }

        protected Position()
        {
        }

        public virtual char File { get; }

        public virtual char Rank { get; }

        public virtual bool Equals(Position other)
        {
            return this.File == other.File
                && this.Rank == other.Rank;
        }

        public override string ToString()
        {
            return $"{this.File}{this.Rank}";
        }
    }
}