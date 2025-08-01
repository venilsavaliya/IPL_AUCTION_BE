using IplAuction.Service.Implementations;

namespace IplAuction.Service.Interface;

public interface IMatchPointservice 
{
    Task<int> GetTotalPointsOfAllPlayersBySeasonId(List<int> players, int seasonId);
}
