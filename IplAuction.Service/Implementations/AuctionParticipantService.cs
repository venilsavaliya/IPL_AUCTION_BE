using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class AuctionParticipantService(IAuctionParticipantRepository auctionParticipantRepo, IMatchPointservice matchPointservice) : IAuctionParticipantService
{
    private readonly IAuctionParticipantRepository _auctionParticipantRepo = auctionParticipantRepo;

    private readonly IMatchPointservice _matchPointservice = matchPointservice;

    public async Task AddParticipantAsync(int auctionId, int userId)
    {
        AuctionParticipants auctionParticipants = new()
        {
            UserId = userId,
            AuctionId = auctionId
        };

        await _auctionParticipantRepo.AddAsync(auctionParticipants);

        await _auctionParticipantRepo.SaveChangesAsync();
    }

    public async Task AddParticipantsAsync(List<AuctionParticipants> auctionParticipants)
    {
        await _auctionParticipantRepo.AddRangeAsync(auctionParticipants);
        await _auctionParticipantRepo.SaveChangesAsync();
    }

    public async Task RemoveParticipantsAsync(List<AuctionParticipants> auctionParticipants)
    {
        _auctionParticipantRepo.RemoveRange(auctionParticipants);

        await _auctionParticipantRepo.SaveChangesAsync();
    }

    public async Task<List<AuctionParticipants>> GetParticipantsByUserIdsAsync(int auctionid, List<int> auctionParticipants)
    {
        return await _auctionParticipantRepo.GetAllWithFilterAsync(ap => ap.AuctionId == auctionid && auctionParticipants.Contains(ap.UserId));
    }

    public async Task<List<AuctionParticipants>> GetParticipantsByAuctionIdAsync(int auctionid)
    {
        return await _auctionParticipantRepo.GetAllWithFilterAsync(ap => ap.AuctionId == auctionid);
    }

    public async Task<List<UserResponseViewModel>> GetAuctionParticipantsByAuctionId(int auctionid)
    {
        return await _auctionParticipantRepo.GetAllParticipantsByAuctionIdAsync(auctionid);
    }

    public async Task<List<AuctionParticipantResponseModel>> GetAllAuctionParticipantsByAuctionId(int auctionid)
    {
        return await _auctionParticipantRepo.GetAuctionParticipants(auctionid);
    }

    public async Task<AuctionParticipantResponseModel> GetAuctionParticipant(AuctionParticipantRequestModel requestModel)
    {
        return await _auctionParticipantRepo.GetAuctionParticipant(requestModel);
    }

    public async Task<List<AuctionTeamResponseModel>> GetAllJoinedTeams(int auctionId)
    {
        return await _auctionParticipantRepo.GetAllJoinedTeams(auctionId);
    }

    public async Task DeductUserBalance(DeductBalanceRequest request)
    {
        AuctionParticipants participant = await _auctionParticipantRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.UserId == request.UserId) ?? throw new NotFoundException(nameof(AuctionParticipants));

        participant.PurseBalance -= request.Amount;

        await _auctionParticipantRepo.SaveChangesAsync();
    }

    // This Will Return List Of AuctionParticipants With Their Name And Total Points For that Season in Which Auction Held
    public async Task<List<AuctionParticipantDetail>> GetAuctionParticipantDetails(AuctionParticipantDetailRequestModel request)
    {
        return await _auctionParticipantRepo.GetAuctionParticipantsDetailList(request);
    }

    public async Task<AuctionParticipantAllDetail> GetAllDetailOfAuctionParticipant(AuctionParticipantAllDetailRequestModel request)
    {
        return await _auctionParticipantRepo.GetAllDetailOfAuctionParticipant(request);
    }

    public async Task<AuctionParticipantPlayerResponseModel> GetParticipantsPlayerListAndDetail(ParticipantPlayerRequestModel request)
    {
        return await _auctionParticipantRepo.GetParticipantsPlayerListAndDetail(request);
    }
}
