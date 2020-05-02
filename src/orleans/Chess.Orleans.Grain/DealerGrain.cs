namespace Chess.Orleans.Grain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Orleans.Contract;

    using global::Orleans;

    using static Chess.Infra.Monad.Utils.Util;

    public class DealerGrain : Grain, IDealerGrain
    {
        private readonly ICollection<IUserGrain> usersGrain = new List<IUserGrain>();
        private bool isFirst;

        public Task<bool> HasBegun() => Task(this.usersGrain.Count > 1);

        public async Task<Try<IUserGrain>> NextUserGrain()
        {
            if (!await this.HasBegun())
            {
                return new ChessException();
            }

            this.isFirst = !this.isFirst;

            return Success(this.isFirst
                ? this.usersGrain.First()
                : this.usersGrain.ElementAt(1));
        }

        public async Task<Try<Unit>> AddUserGrain(Option<IUserGrain> userGrainOption)
        {
            if (await this.HasBegun())
            {
                return new ChessException();
            }

            this.usersGrain.AddItem(userGrainOption);

            return Unit();
        }
    }
}
