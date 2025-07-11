using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Match;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class MatchService(IMatchRepository matchRepository) :IMatchService
{

    private readonly IMatchRepository _matchRepository = matchRepository;


    public async Task AddMatch(MatchRequest request)
    {
        Match match = new()
        {
            TeamAId = request.TeamAId,
            TeamBId = request.TeamBId,
            StartDate = request.StartDate
        };

        await _matchRepository.AddAsync(match);
        await _matchRepository.SaveChangesAsync();
    }

    public async Task DeleteMatch(int id)
    {
        Match match = await _matchRepository.GetWithFilterAsync(m => m.Id == id) ?? throw new NotFoundException(nameof(Match));

        match.IsDeleted = true;

        await _matchRepository.SaveChangesAsync();
    }

    public async Task<List<MatchResponse>> GetAllMatch()
    {
        List<MatchResponse> response = await _matchRepository.GetAllWithFilterAsync(m => m.IsDeleted == false, m => new MatchResponse()
        {
            MatchId = m.Id,
            TeamAId = m.TeamAId,
            TeamBId = m.TeamBId,
            TeamAName = m.TeamA.Name,
            TeamBName = m.TeamB.Name,
            StartDate = m.StartDate
        });
        return response;
    }
}
