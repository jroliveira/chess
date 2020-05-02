namespace Chess.Domain.Shared
{
    using System;

    using static System.Char;
    using static System.Convert;
    using static System.Math;

    using static Chess.Domain.Shared.Direction;

    internal readonly struct Position : IEquatable<Position>
    {
        private Position(char file, byte rank)
        {
            this.File = file;
            this.Rank = rank;
        }

        internal char File { get; }

        internal byte Rank { get; }

        public static implicit operator Position(string position)
        {
            var file = position[0];
            var rank = ToByte(GetNumericValue(position[1]));

            return new Position(file, rank);
        }

        public static implicit operator string(Position position) => position.ToString();

        public bool Equals(Position other) => this.File == other.File && this.Rank == other.Rank;

        public override string ToString() => $"{this.File}{this.Rank}";

        internal (byte FilesToMove, byte RanksToMove) GetDistanceTo(Position newPosition) => (
            ToByte(Abs(this.File - newPosition.File)),
            ToByte(Abs(this.Rank - newPosition.Rank)));

        internal (Direction Horizontal, Direction Vertical) GetDirectionTo(Position newPosition)
        {
            var files = this.File - newPosition.File;
            var ranks = this.Rank - newPosition.Rank;
            var position = (files, ranks);

            return position switch
            {
                var moves when moves.ranks < 0 && moves.files == 0 => (Top, None),
                var moves when moves.ranks < 0 && moves.files > 0 => (Top, Left),
                var moves when moves.ranks < 0 && moves.files < 0 => (Top, Right),
                var moves when moves.ranks > 0 && moves.files < 0 => (Bottom, Right),
                var moves when moves.ranks > 0 && moves.files > 0 => (Bottom, Left),
                var moves when moves.ranks > 0 && moves.files == 0 => (Bottom, None),
                var moves when moves.ranks == 0 && moves.files > 0 => (None, Left),
                var moves when moves.ranks == 0 && moves.files < 0 => (None, Right),
                _ => (None, None)
            };
        }
    }
}
