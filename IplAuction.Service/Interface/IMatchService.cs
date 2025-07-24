using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.Match;

namespace IplAuction.Service.Interface;

public interface IMatchService
{
    Task AddMatch(MatchRequest request);

    Task DeleteMatch(int id);

    Task<List<MatchResponse>> GetAllMatch();

    Task<PaginatedResult<MatchResponse>> GetFilteredMatchAsync(MatchFilterParams filterParams);
    Task<MatchResponse> GetMatchById(int id);

    Task UpdateMatch(UpdateMatchRequest request);
    Task<LiveMatchStatusResponse> GetLiveMatchStatus(int matchId);
}
