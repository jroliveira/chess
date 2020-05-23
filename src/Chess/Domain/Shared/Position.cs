namespace Chess.Domain.Shared
{
    using System;

    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Extensions;

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

        public static implicit operator Position(string position) => CreatePosition(position)
            .GetOrElse(new Position('?', 0));

        public static implicit operator string(Position position) => position.ToString();

        public bool Equals(Position other) => this.File == other.File && this.Rank == other.Rank;

        public override string ToString() => $"{this.File}{this.Rank}";

        internal static Try<Position> CreatePosition(string position)
        {
            try
            {
                var file = position[0];
                var rank = ToByte(GetNumericValue(position[1]));

                return new Position(file, rank);
            }
            catch (Exception exception)
            {
                return new ChessException(exception.Message);
            }
        }

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
                var moves when moves.ranks < 0 && moves.files == 0 => (Top, Direction.None),
                var moves when moves.ranks < 0 && moves.files > 0 => (Top, Left),
                var moves when moves.ranks < 0 && moves.files < 0 => (Top, Right),
                var moves when moves.ranks > 0 && moves.files < 0 => (Bottom, Right),
                var moves when moves.ranks > 0 && moves.files > 0 => (Bottom, Left),
                var moves when moves.ranks > 0 && moves.files == 0 => (Bottom, Direction.None),
                var moves when moves.ranks == 0 && moves.files > 0 => (Direction.None, Left),
                var moves when moves.ranks == 0 && moves.files < 0 => (Direction.None, Right),
                _ => (Direction.None, Direction.None)
            };
        }
    }
}
