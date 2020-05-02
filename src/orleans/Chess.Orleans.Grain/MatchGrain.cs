namespace Chess.Orleans.Grain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Chess.Infra.Monad;
    using Chess.Infra.Monad.Linq;
    using Chess.Orleans.Contract;

    using global::Orleans;

    using static System.Guid;

    using static Chess.Infra.Monad.Utils.Util;

    public class MatchGrain : Grain, IMatchGrain
    {
        private readonly HashSet<IUserGrain> usersGrain = new HashSet<IUserGrain>();
        private readonly IGame game = new Game();

        private IDealerGrain dealerGrain = new DealerGrain();

        public override async Task OnActivateAsync()
        {
            this.dealerGrain = this.GrainFactory.GetGrain<IDealerGrain>(NewGuid());

            var registry = this.GrainFactory.GetGrain<IMatchRegistryGrain>("match_registry");
            await registry.AddMatchGrain(this);
        }

        public Task<Unit> WakeUp() => Task(Unit());

        public Task<bool> HasBegun() => this.dealerGrain.HasBegun();

        public Task<Try<Match>> JoinUser(
            Option<IUserGrain> userGrainOption,
            Option<string> userNameOption,
            Option<PieceColor> playingWithOption = default) => this.game
                .JoinUser(userNameOption, playingWithOption)
                .Match(
                    exception => Task(Failure<Match>(exception)),
                    async match =>
                    {
                        this.usersGrain.AddItem(userGrainOption);

                        var unitOption = await this.dealerGrain.AddUserGrain(userGrainOption);
                        unitOption.Select(async _ =>
                        {
                            var nextUserGrainOption = await this.dealerGrain.NextUserGrain();
                            return nextUserGrainOption.Select(nextUserGrain =>
                            {
                                nextUserGrain.YourMove(match, this);
                                return Unit();
                            });
                        });

                        return Success(match);
                    });

        public Task<Try<Match>> MovePiece(
            Option<string> piecePositionOption,
            Option<string> newPositionOption,
            Option<string> userNameOption) => this.game
                .MovePiece(piecePositionOption, newPositionOption, userNameOption)
                .Match(
                    _ => Task(Failure<Match>(_)),
                    async match =>
                    {
                        foreach (var userGrain in this.usersGrain)
                        {
                            userGrain.GameChanged(match);
                        }

                        var nextUserGrainOption = await this.dealerGrain.NextUserGrain();
                        nextUserGrainOption.Select(nextUserGrain =>
                        {
                            nextUserGrain.YourMove(match, this);
                            return Unit();
                        });

                        return Success(match);
                    });
    }
}
