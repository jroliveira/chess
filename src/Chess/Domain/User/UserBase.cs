namespace Chess.Domain.User
{
    using System;

    internal abstract class UserBase : IEquatable<UserBase>
    {
        private readonly string name;

        protected UserBase(string name, UserType type)
        {
            this.Type = type;
            this.name = name;
        }

        internal UserType Type { get; }

        public static implicit operator string(UserBase user) => user.ToString();

        public override string ToString() => this.name;

        public bool Equals(UserBase other) => this.Equals(other?.name);

        internal bool Equals(string? other) => this.name.Equals(other);
    }
}
