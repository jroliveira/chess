namespace Chess.Models
{
    public sealed class Player
    {
        private readonly string name;

        public Player(string name, bool isWhitePiece)
        {
            this.name = name;
            this.IsWhitePiece = isWhitePiece;
        }

        public bool IsWhitePiece { get; }

        public static implicit operator string(Player player) => player.name;

        public override string ToString() => this.name;
    }
}
