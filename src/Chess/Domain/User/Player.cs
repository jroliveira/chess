namespace Chess.Domain.User
{
    using System;

    using Chess;

    using static Chess.User;

    internal sealed class Player : UserBase, IEquatable<Player>
    {
        private Player(string name, PieceColor playingWith)
            : base(name, UserType.Player) => this.PlayingWith = playingWith;

        internal PieceColor PlayingWith { get; }

        public static implicit operator PieceColor(Player player) => player.PlayingWith;

        public static implicit operator User(Player player) => player.ToUser();

        public bool Equals(Player? other) => this.Equals(other?.PlayingWith) && this.Equals(other?.ToString());

        internal static Player CreatePlayer(string name, PieceColor playingWith) => new Player(name, playingWith);

        internal bool Equals(PieceColor? playingWith) => this.PlayingWith.Equals(playingWith);

        internal User ToUser() => CreateUser(this.ToString(), this.Type, this.PlayingWith);
    }
}
