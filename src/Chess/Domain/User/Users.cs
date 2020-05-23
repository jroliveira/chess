namespace Chess.Domain.User
{
    using System.Collections.Generic;
    using System.Linq;

    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Extensions;

    using static System.String;

    using static Chess.Constants.ErrorMessages;
    using static Chess.Constants.ErrorMessages.UserError;
    using static Chess.Domain.User.Player;
    using static Chess.Domain.User.Spectator;
    using static Chess.Infra.Monad.Utils.Util;
    using static Chess.PieceColor;

    internal sealed class Users
    {
        private readonly IDictionary<string, UserBase> users;

        internal Users() => this.users = new Dictionary<string, UserBase>();

        internal Users(IEnumerable<UserBase> users) => this.users = users.ToDictionary(user => user.ToString(), user => user);

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                var players = new List<Player>();

                foreach (var userBase in this.users)
                {
                    if (userBase.Value is Player player)
                    {
                        players.Add(player);
                    }
                }

                return players;
            }
        }

        public IReadOnlyCollection<Spectator> Spectators
        {
            get
            {
                var spectators = new List<Spectator>();

                foreach (var userBase in this.users)
                {
                    if (userBase.Value is Spectator player)
                    {
                        spectators.Add(player);
                    }
                }

                return spectators;
            }
        }

        internal Try<UserBase> AddUser(Option<string> nameOption, Option<PieceColor> playingWithOption)
        {
            var name = nameOption.GetOrElse(Empty);
            if (name == Empty)
            {
                return CannotBeNullOrEmpty("User name");
            }

            if (this.users.Any(item => item.Value.Equals(name)))
            {
                return IsAlreadyInUse(name);
            }

            UserBase newUser;

            if (this.users.Count > 1)
            {
                newUser = CreateSpectator(name);
                this.users.Add(newUser, newUser);

                return newUser;
            }

            var playingWith = playingWithOption.GetOrElse(NonePiece);
            if (playingWith == NonePiece)
            {
                return CannotBeNullOrEmpty("User piece");
            }

            if (this.Players.Any(player => player.Equals(playingWith)))
            {
                return IsAlreadyInUse(playingWith);
            }

            newUser = CreatePlayer(name, playingWith);
            this.users.Add(newUser, newUser);

            return newUser;
        }

        internal Try<UserBase> GetUser(Option<string> nameOption) => nameOption
            .Fold(Failure<UserBase>(CannotBeNullOrEmpty("User")))(name => this.users.TryGetValue(name, out var user)
                ? Success(user)
                : IsNotPlaying(name));
    }
}
