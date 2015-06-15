using System;

namespace Chess
{
    internal class Position : IEquatable<Position>
    {
        public virtual char File { get; private set; }
        public virtual char Rank { get; private set; }

        protected Position()
        {

        }

        public Position(char file, char rank)
        {
            File = file;
            Rank = rank;
        }

        public virtual bool Equals(Position other)
        {
            return File == other.File
                   && Rank == other.Rank;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", File, Rank);
        }
    }
}