namespace Chess.Interfaces
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IDealer : IGrainWithGuidKey
    {
        Task<IPlayerCallback> NextPlayer();

        Task AddPlayer(IPlayerCallback player);

        //Task SetBoard(IBoard board);

        //Task GameReadyAsync();
    }
}
