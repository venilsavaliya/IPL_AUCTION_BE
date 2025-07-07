using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.AuctionParticipant;
using IplAuction.Entities.ViewModels.UserTeam;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class UserTeamService(IUserTeamRepository userTeamRepository, IAuctionService auctionService, IAuctionParticipantService auctionParticipantService) : IUserTeamService
{
    private readonly IUserTeamRepository _userTeamRepository = userTeamRepository;
    private readonly IAuctionService _auctionService = auctionService;
    private readonly IAuctionParticipantService _auctionParticipantService = auctionParticipantService;

    public async Task AddUserTeam(AddUserTeamRequestModel request)
    {
        // Remove Current Player Being Auctioned From The Auction After Getting Sold 
        await _auctionService.RemoveCurrentPlayerFromAuction(request.AuctionId);

        // Adjust The Purse Balance Of The User
        DeductBalanceRequest balanceRequest = new()
        {
            AuctionId = request.AuctionId,
            Amount = request.Price,
            UserId = request.UserId
        };
        await _auctionParticipantService.DeductUserBalance(balanceRequest);

        UserTeam userTeam = new()
        {
            UserId = request.UserId,
            AuctionId = request.AuctionId,
            PlayerId = request.PlayerId,
            Price = request.Price
        };

        await _userTeamRepository.AddAsync(userTeam);

        await _userTeamRepository.SaveChangesAsync();
    }

    public async Task<List<UserTeamResponseModel>> GetUserTeams(UserTeamRequestModel request)
    {
        return await _userTeamRepository.GetUserTeams(request);
    }
}
