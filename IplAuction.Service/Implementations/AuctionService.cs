using IplAuction.Entities.DTOs;
using IplAuction.Entities.Enums;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Auction;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Service.Implementations;

public class AuctionService(IAuctionRepository auctionRepository, ICurrentUserService currentUser, IUserRepository userRepository, IGenericRepository<AuctionParticipants> auctionParticipantRepo, IGenericRepository<AuctionPlayer> auctionPlayerRepo, IPlayerRepository playerRepository) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;

    private readonly ICurrentUserService _currentUser = currentUser;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IPlayerRepository _playerRepository = playerRepository;

    private readonly IGenericRepository<AuctionParticipants> _auctionParticipantRepo = auctionParticipantRepo;

    private readonly IGenericRepository<AuctionPlayer> _auctionPlayerRepo = auctionPlayerRepo;

    public async Task AddAuctionAsync(AddAuctionRequestModel request)
    {
        // Get UserId From The Jwt Claims
        int? UserId = _currentUser.UserId;

        Auction auction = new()
        {
            Title = request.Title,
            ManagerId = (int)UserId,
            StartDate = request.StartDate,
            AuctionStatus = request.AuctionStatus,
            MinimumBidIncreament = request.MinimumBidIncreament,
            MaximumPurseSize = request.MaximumPurseSize
        };

        await _auctionRepository.AddAsync(auction);
        await _auctionRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAuctionAsync(int id)
    {
        Auction? auction = await _auctionRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Auction));

        auction.IsDeleted = true;

        await _auctionRepository.SaveChangesAsync();

        return true;
    }

    // public async Task<List<AuctionResponseModel>> GetFilteredAuctionAsync(AuctionFilterModel auctionFilterModel)
    // {
    //     List<AuctionResponseModel> auctions = await _auctionRepository.GetFilteredAuctionsAsync()
    // }

    public async Task<List<AuctionResponseModel>> GetAllAuctionAsync()
    {
        List<AuctionResponseModel> auctions = await _auctionRepository.GetAllWithFilterAsync(a => a.IsDeleted == false, a => new AuctionResponseModel
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
        AuctionResponseModel auction = await _auctionRepository.GetWithFilterAsync(a => a.IsDeleted == false, a => new AuctionResponseModel
        {
            Id = a.Id,
            ManagerId = a.ManagerId,
            StartDate = a.StartDate,
            AuctionStatus = a.AuctionStatus,
            Title = a.Title
        }) ?? throw new NotFoundException(nameof(Auction));

        return auction;
    }

    public async Task UpdateAuctionAsync(UpdateAuctionRequestModel request)
    {
        Auction? auction = await _auctionRepository.GetWithFilterAsync(a => a.IsDeleted == false && a.Id == request.Id) ?? throw new NotFoundException(nameof(Auction));

        auction.Title = request.Title;
        auction.UpdatedAt = DateTime.UtcNow;
        auction.StartDate = request.StartDate;
        auction.AuctionStatus = request.AuctionStatus;
        auction.MinimumBidIncreament = request.MinimumBidIncreament;
        auction.MaximumPurseSize = request.MaximumPurseSize;

        await _auctionRepository.SaveChangesAsync();
    }

    public async Task<bool> JoinAuctionAsync(int id)
    {
        // Get UserId From The Jwt Claims
        int? userId = _currentUser.UserId;

        Auction auction = await _auctionRepository.GetWithFilterAsync(a => a.IsDeleted == false && a.Id == id) ?? throw new NotFoundException(nameof(Auction));

        // Ensure That User Join Auction Before it Starts
        if (auction.AuctionStatus != AuctionStatus.Scheduled)
            return false;

        AuctionParticipants auctionParticipants = new()
        {
            UserId = (int)userId,
            AuctionId = id
        };

        await _auctionParticipantRepo.AddAsync(auctionParticipants);

        await _auctionParticipantRepo.SaveChangesAsync();

        return true;
    }

    public async Task<PlayerResponseModel> GetRandomUnAuctionedPlayer(int auctionId)
    {
        IQueryable<AuctionPlayer> players = _auctionPlayerRepo.GetAllQueryableWithFilterAsync(p => p.IsAuctioned == false && p.AuctionId == auctionId);

        AuctionPlayer auctionPlayer = await players.OrderBy(p => Guid.NewGuid()).FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(AuctionPlayer));

        Player? player = await _playerRepository.GetWithFilterAsync(p => p.Id == auctionPlayer.PlayerId) ?? throw new NotFoundException(nameof(Player));

        PlayerResponseModel response = new()
        {
            Id = player.Id,
            BasePrice = player.BasePrice,
            Image = player.Image,
            Name = player.Name,
            Skill = player.Skill.ToString(),
            TeamId = player.TeamId,
            DateOfBirth = player.DateOfBirth,
        };

        return response;
    }

    public async Task AddPlayerToAuction(ManageAuctionPlayerRequest request)
    {
        AuctionPlayer auctionPlayer = new()
        {
            PlayerId = request.PlayerId,
            AuctionId = request.AuctionId
        };

        await _auctionPlayerRepo.AddAsync(auctionPlayer);

        await _auctionPlayerRepo.SaveChangesAsync();
    }

    public async Task RemovePlayerFromAuction(ManageAuctionPlayerRequest request)
    {
        AuctionPlayer auctionPlayer = await _auctionPlayerRepo.GetWithFilterAsync(ap => ap.AuctionId == request.AuctionId && ap.PlayerId == request.PlayerId) ?? throw new NotFoundException(nameof(AuctionPlayer));

        _auctionPlayerRepo.Delete(auctionPlayer);

        await _auctionRepository.SaveChangesAsync();
    }
}
