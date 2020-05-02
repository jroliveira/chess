namespace Chess.Orleans.Contract
{
    using System.Threading.Tasks;

    using Chess.Infra.Monad;

    using global::Orleans;

    public interface IMatchGrain : IGrainWithStringKey
    {
        Task<Unit> WakeUp();

        Task<bool> HasBegun();

        Task<Try<Match>> JoinUser(
            Option<IUserGrain> userGrainOption,
            Option<string> userNameOption,
            Option<PieceColor> playingWithOption = default);

        Task<Try<Match>> MovePiece(
            Option<string> piecePositionOption,
            Option<string> newPositionOption,
            Option<string> userNameOption);
    }
}
