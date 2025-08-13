using IplAuction.Entities.DTOs;
using IplAuction.Entities.ViewModels.AuctionParticipant;
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

    Task UpdateMatchInningNumber(int matchId, int inningNumber);

    Task ChangeMatchInning(int matchId, int inningNumber);

    Task<LiveMatchStatusResponse> GetLiveMatchStatus(int matchId);
    
    Task<int> GetSeasonIdFromMatchId(int matchId);

    Task<List<AuctionParticipantMantchDetail>> GetAuctionParticipantMantchDetailsAsync(int auctionId, int userId);
}
