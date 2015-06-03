using System;

namespace Chess.Game
{
    internal class Position : IEquatable<Position>
    {
        public char File { get; private set; }
        public char Rank { get; private set; }

        public Position(char file, char rank)
        {
            File = file;
            Rank = rank;
        }

        public bool Equals(Position other)
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