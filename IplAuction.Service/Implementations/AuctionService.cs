using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Entities.ViewModels.User;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
namespace IplAuction.Service.Implementations;

public class AuctionService(IAuctionRepository auctionRepository, ICurrentUserService currentUser, IPlayerService playerService, IAuctionParticipantService auctionParticipantService, IUserService userService, IUnitOfWork unitOfWork, IAuctionPlayerService auctionPlayerService) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;

    private readonly ICurrentUserService _currentUser = currentUser;

    private readonly IPlayerService _playerService = playerService;

    private readonly IAuctionParticipantService _auctionParticipantService = auctionParticipantService;

    private readonly IUserService _userService = userService;

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly IAuctionPlayerService _auctionPlayerService = auctionPlayerService;

    public async Task AddAuctionAsync(AddAuctionRequestModel request)
    {
        // Get UserId From The Jwt Claims
        int? UserId = _currentUser.UserId;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Auction auction = new()
            {
                Title = request.Title,
                ManagerId = (int)UserId,
                StartDate = request.StartDate,
                AuctionStatus = request.AuctionStatus,
                MinimumBidIncreament = request.MinimumBidIncreament,
                MaximumPurseSize = request.MaximumPurseSize,
                MaximumTeamsCanJoin = request.MaximumTeamsCanJoin,
                ModeOfAuction = request.AuctionMode
            };

            await _auctionRepository.AddAsync(auction);
            await _auctionRepository.SaveChangesAsync();

            var auctionParticipants = request.ParticipantUserIds.Select(uid => new AuctionParticipants
            {
                AuctionId = auction.Id,
                UserId = uid
            }).ToList();

            await _auctionParticipantService.AddParticipantsAsync(auctionParticipants);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAuctionAsync(int id)
    {
        Auction? auction = await _auctionRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Auction));

        auction.IsDeleted = true;

        await _auctionRepository.SaveChangesAsync();

        return true;
    }

    public async Task<List<AuctionResponseModel>> GetAllAuctionAsync()
    {
        int userId = _currentUser.UserId;

        UserResponseViewModel user = await _userService.GetByIdAsync(userId) ?? throw new NotFoundException(nameof(User));

        // Here Admin Can Fetch All the Auction And Manager Can Fetch The Auction Which Created By Him
        List<AuctionResponseModel> auctions = await _auctionRepository.GetAllWithFilterAsync(a => a.IsDeleted == false && (user.Role == UserRole.Admin || a.ManagerId == userId), a => new AuctionResponseModel
        {
            Id = a.Id,
            ManagerId = a.ManagerId,
            StartDate = a.StartDate,
            AuctionStatus = a.AuctionStatus,
            Title = a.Title
        });

        return auctions;
    }

    public async Task<PaginatedResult<AuctionResponseModel>> GetAuctionsAsync(AuctionFilterParam filterParams)
    {
        return await _auctionRepository.GetFilteredAuctionsAsync(filterParams);
    }

    public async Task<AuctionResponseModel> GetAuctionByIdAsync(int id)
    {
        AuctionResponseModel auction = await _auctionRepository.GetAuctionById(id);

        return auction;
    }

    public async Task<List<UserResponseViewModel>> GetAllTeamsOfAuction(int auctionId)
    {
        List<UserResponseViewModel> teams = await _auctionParticipantService.GetAuctionParticipantsByAuctionId(auctionId);

        return teams;
    }


    public async Task UpdateAuctionAsync(UpdateAuctionRequestModel request)
    {
        Auction? auction = await _auctionRepository.GetWithFilterAsync(a => a.IsDeleted == false && a.Id == request.Id) ?? throw new NotFoundException(nameof(Auction));

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            List<AuctionParticipants> oldParticipants = await _auctionParticipantService.GetParticipantsByAuctionIdAsync(auction.Id);

            List<int> oldParticipantUserIds = oldParticipants.Select(p => p.UserId).ToList();
            List<int> newParticipantUserIds = request.ParticipantUserIds;

            var toAdd = newParticipantUserIds.Except(oldParticipantUserIds).ToList();
            var toRemove = oldParticipantUserIds.Except(newParticipantUserIds).ToList();

            // Remove Users
            if (toRemove.Count != 0)
            {
                List<AuctionParticipants> participantsToRemove = await _auctionParticipantService.GetParticipantsByUserIdsAsync(auction.Id, toRemove);

                if (participantsToRemove.Count > 0)
                {
                    await _auctionParticipantService.RemoveParticipantsAsync(participantsToRemove);
                }
            }

            // Add New Users
            if (toAdd.Count != 0)
            {
                List<AuctionParticipants> participantsToAdd = toAdd.Select(uid => new AuctionParticipants
                {
                    AuctionId = auction.Id,
                    UserId = uid
                }).ToList();

                await _auctionParticipantService.AddParticipantsAsync(participantsToAdd);
            }

            auction.Title = request.Title;
            auction.UpdatedAt = DateTime.UtcNow;
            auction.StartDate = request.StartDate;
            auction.AuctionStatus = request.AuctionStatus;
            auction.MinimumBidIncreament = request.MinimumBidIncreament;
            auction.MaximumPurseSize = request.MaximumPurseSize;
            auction.MaximumTeamsCanJoin = request.MaximumTeamsCanJoin;
            auction.ModeOfAuction = request.AuctionMode;

            await _auctionRepository.SaveChangesAsync();

            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


    }

    public async Task<bool> JoinAuctionAsync(int id)
    {
        // Get UserId From The Jwt Claims
        int userId = _currentUser.UserId;

        Auction auction = await _auctionRepository.GetWithFilterAsync(a => a.IsDeleted == false && a.Id == id) ?? throw new NotFoundException(nameof(Auction));

        // Ensure That User Join Auction Before it Starts
        if (auction.AuctionStatus != AuctionStatus.Scheduled)
            return false;

        await _auctionParticipantService.AddParticipantAsync(auction.Id, userId);

        // AuctionParticipants auctionParticipants = new()
        // {
        //     UserId = (int)userId,
        //     AuctionId = id
        // };

        // await _auctionParticipantRepo.AddAsync(auctionParticipants);

        // await _auctionParticipantRepo.SaveChangesAsync();

        return true;
    }

    public async Task<PlayerResponseModel> GetCurrentAuctionPlayer(int auctionId)
    {
        Auction auction = await _auctionRepository.GetWithFilterAsync(a => a.Id == auctionId) ?? throw new NotFoundException(nameof(Auction));

        PlayerResponseModel player = await _playerService.GetPlayerByIdAsync(auction.CurrentPlayerId);

        return player;
    }

  

    // public async Task<PlayerResponseDetailModel> GetRandomUnAuctionedPlayer(int auctionId)
    // {
    //     IQueryable<AuctionPlayer> players = _auctionPlayerRepo.GetAllQueryableWithFilterAsync(p => p.IsAuctioned == false && p.AuctionId == auctionId);

    //     AuctionPlayer auctionPlayer = await players.OrderBy(p => Guid.NewGuid()).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(AuctionPlayer));

    //     // Player? player = await _playerRepository.GetWithFilterAsync(p => p.Id == auctionPlayer.PlayerId) ?? throw new NotFoundException(nameof(Player));

    //     PlayerResponseDetailModel player = await _playerService.GetPlayerByIdAsync(auctionPlayer.Id);

    //     // PlayerResponseModel response = new()
    //     // {
    //     //     PlayerId = player.Id,
    //     //     BasePrice = player.BasePrice,
    //     //     ImageUrl = player.Image,
    //     //     Name = player.Name,
    //     //     Skill = player.Skill,
    //     //     Age = CalculateAge.CalculateAgeFromDbo(player.DateOfBirth),
    //     //     Country = player.Country
    //     // };

    //     return player;
    // }

    // public async Task AddPlayerToAuction(ManageAuctionPlayerRequest request)
    // {
    //     await _auctionPlayerService.AddAuctionPlayer(request);
    // }

    // public async Task RemovePlayerFromAuction(ManageAuctionPlayerRequest request)
    // {
    //     AuctionPlayer auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId) ?? throw new NotFoundException(nameof(AuctionPlayer));

    //     _auctionPlayerRepo.Delete(auctionPlayer);

    //     await _auctionRepository.SaveChangesAsync();
    // }
}
