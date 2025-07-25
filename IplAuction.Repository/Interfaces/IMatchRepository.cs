
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Match;

namespace IplAuction.Repository.Interfaces;

public interface IMatchRepository : IGenericRepository<Match>
{
    Task<PaginatedResult<MatchResponse>> GetFilteredMatchAsync(MatchFilterParams filterParams);

    Task<MatchResponse> GetById(int id);
    
}
