namespace Chess.Interfaces
{
    using System.Threading.Tasks;

    using Orleans;

    public interface IDealer : IGrainWithGuidKey
    {
        Task<IPlayer> NextPlayer();

        Task AddPlayer(IPlayer player);
    }
}
