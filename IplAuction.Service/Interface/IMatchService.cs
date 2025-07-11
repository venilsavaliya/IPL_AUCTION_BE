using IplAuction.Entities.ViewModels.Match;

namespace IplAuction.Service.Interface;

public interface IMatchService
{
    Task AddMatch(MatchRequest request);

    Task DeleteMatch(int id);

    Task<List<MatchResponse>> GetAllMatch();
}
