namespace Chess
{
    using Chess.Infra.Monad;

    public sealed class User
    {
        private readonly string name;

        private User(
            string name,
            UserType type,
            Option<PieceColor> playingWithOption = default)
        {
            this.name = name;
            this.Type = type;
            this.PlayingWithOption = playingWithOption;
        }

        public UserType Type { get; }

        public Option<PieceColor> PlayingWithOption { get; }

        public static implicit operator string(User user) => user.name;

        public static implicit operator Option<PieceColor>(User user) => user.PlayingWithOption;

        public override string ToString() => this.name;

        internal static User CreateUser(
            string name,
            UserType type,
            Option<PieceColor> playingWithOption = default) => new User(
                name,
                type,
                playingWithOption);
    }
}
