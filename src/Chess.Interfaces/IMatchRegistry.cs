
namespace Chess.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Orleans;

    public interface IMatchRegistry : IGrainWithStringKey
    {
        Task AddMatch(IMatch match);

        Task<List<IMatch>> GetAllMatches();
    }
}
