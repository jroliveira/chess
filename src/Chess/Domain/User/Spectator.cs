namespace Chess.Domain.User
{
    using System;

    using Chess;

    using static Chess.User;

    internal sealed class Spectator : UserBase, IEquatable<Spectator>
    {
        private Spectator(string name)
            : base(name, UserType.Spectator)
        {
        }

        public static implicit operator User(Spectator spectator) => spectator.ToUser();

        public bool Equals(Spectator? other) => this.Equals(other?.ToString());

        internal static Spectator CreateSpectator(string name) => new Spectator(name);

        internal User ToUser() => CreateUser(this.ToString(), this.Type);
    }
}
