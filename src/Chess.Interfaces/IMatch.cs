namespace Chess.Interfaces
{
    using System.Threading.Tasks;

    using Chess.Lib.Monad;

    using Orleans;

    public interface IMatch : IGrainWithStringKey
    {
        Task WakeUp();

        Task<Try<Unit>> JoinPlayer(IPlayer player, Option<string> playerName);

        Task<Try<Unit>> Start();

        Task<Try<Unit>> MovePiece(Option<string> piecePosition, Option<string> newPosition, Option<string> playerName);
    }
}
