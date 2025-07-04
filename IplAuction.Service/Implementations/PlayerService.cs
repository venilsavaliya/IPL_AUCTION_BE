using System.Globalization;
using System.Text;
using CsvHelper;
using IplAuction.Entities.DTOs;
using IplAuction.Entities.Exceptions;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Player;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Implementations;

public class PlayerService(IFileStorageService fileStorageService, IPlayerRepository playerRepository) : IPlayerService
{
    private readonly IPlayerRepository _playerRepository = playerRepository;

    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task AddPlayerAsync(AddPlayerRequest player)
    {
        string? imageUrl = "";

        if (player.Image != null)
        {
            imageUrl = await _fileStorageService.UploadFileAsync(player.Image);
        }

        Player NewPlayer = new()
        {
            Name = player.Name,
            DateOfBirth = player.DateOfBirth,
            Country = player.Country,
            Skill = player.Skill,
            BasePrice = player.BasePrice,
            TeamId = player.TeamId,
            Image = !string.IsNullOrEmpty(imageUrl) ? imageUrl : null,
            CreatedAt = DateTime.UtcNow,
        };

        await _playerRepository.AddAsync(NewPlayer);

        await _playerRepository.SaveChangesAsync();
    }

    public async Task ImportPlayersFromCsvAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Uploaded file is empty");

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<PlayerMap>();

        var players = csv.GetRecords<Player>().ToList();

        await _playerRepository.AddPlayersAsync(players);
    }

    public async Task DeletePlayerAsync(int id)
    {
        Player player = await _playerRepository.FindAsync(id) ?? throw new NotFoundException(nameof(Player));

        player.IsDeleted = true;

        await _playerRepository.SaveChangesAsync();
    }

    public async Task<PlayerResponseDetailModel> GetPlayerDetailByIdAsync(int id)
    {
        PlayerResponseDetailModel player = await _playerRepository.GetWithFilterAsync(p => p.IsDeleted == false && p.Id == id, p => new PlayerResponseDetailModel
        {
            PlayerId = p.Id,
            Name = p.Name,
            ImageUrl = p.Image,
            BasePrice = p.BasePrice,
            DateOfBirth = p.DateOfBirth,
            Country = p.Country,
            IsActive = p.IsActive,
            TeamId = p.TeamId,
            Skill = p.Skill
        }) ?? throw new NotFoundException(nameof(Player));

        return player;
    }
    public async Task<PlayerResponseModel> GetPlayerByIdAsync(int id)
    {
        PlayerResponseModel player = await _playerRepository.GetWithFilterAsync(p => p.IsDeleted == false && p.Id == id, p => new PlayerResponseModel(p)) ?? throw new NotFoundException(nameof(Player));

        return player;
    }

    public async Task UpdatePlayerAsync(UpdatePlayerRequest player)
    {
        Player existingPlayer = await _playerRepository.FindAsync(player.Id) ?? throw new NotFoundException(nameof(Player));

        if (player.Image != null)
        {
            var imageUrl = await _fileStorageService.UploadFileAsync(player.Image);
            existingPlayer.Image = imageUrl;
        }

        existingPlayer.Name = player.Name;
        existingPlayer.DateOfBirth = player.DateOfBirth;
        existingPlayer.IsActive = player.IsActive;
        existingPlayer.Country = player.Country;
        existingPlayer.Skill = player.Skill;
        existingPlayer.BasePrice = player.BasePrice;
        existingPlayer.TeamId = player.TeamId;
        existingPlayer.UpdatedAt = DateTime.UtcNow;

        await _playerRepository.SaveChangesAsync();
    }

    public async Task<PaginatedResult<PlayerResponseModel>> GetPlayersAsync(PlayerFilterParams filterParams)
    {
        return await _playerRepository.GetFilteredPlayersAsync(filterParams);
    }

    public async Task UpdatePlayerStatusAsync(UpdatePlayerStatusRequest request)
    {
        Player existingPlayer = await _playerRepository.FindAsync(request.PlayerId) ?? throw new NotFoundException(nameof(Player));

        existingPlayer.IsActive = request.Status;

        await _playerRepository.SaveChangesAsync();
    }

    public async Task<PlayerResponseModel> GetRadomUnAuctionedPlayer(int auctionId)
    {
        PlayerResponseModel player = await _playerRepository.GetRadomUnAuctionedPlayer(auctionId);

        return player;
    }

    public async Task<PlayerResponseModel> GetCurrentAuctionPlayer(int auctionId)
    {
        PlayerResponseModel player = await _playerRepository.GetRadomUnAuctionedPlayer(auctionId);

        return player;
    }
    
}
