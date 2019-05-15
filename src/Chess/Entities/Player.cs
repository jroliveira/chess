namespace Chess.Entities
{
    using System;

    internal sealed class Player : IEquatable<Player>
    {
        private Player(bool useWhitePiece) => this.UseWhitePiece = useWhitePiece;

        internal bool UseWhitePiece { get; set; }

        public static implicit operator string(Player player) => player.ToString();

        public static implicit operator Player(bool useWhitePiece) => new Player(useWhitePiece);

        public override string ToString() => this.UseWhitePiece ? "WhitePiece" : "BlackPiece";

        public bool Equals(Player other) => this.UseWhitePiece.Equals(other.UseWhitePiece);
    }
}
