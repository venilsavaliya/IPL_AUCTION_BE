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

    public async Task<List<PlayerResponseModel>> GetAllPlayersAsync()
    {
        List<PlayerResponseModel> players = await _playerRepository.GetAllWithFilterAsync(p => p.IsDeleted == false, p => new PlayerResponseModel
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            BasePrice = p.BasePrice,
            DateOfBirth = p.DateOfBirth,
            TeamId = p.TeamId,
            Skill = p.Skill.ToString()
        });

        return players;
    }

    public async Task<PlayerResponseModel> GetPlayerByIdAsync(int id)
    {
        PlayerResponseModel player = await _playerRepository.GetWithFilterAsync(p => p.IsDeleted == false && p.Id == id, p => new PlayerResponseModel
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            BasePrice = p.BasePrice,
            DateOfBirth = p.DateOfBirth,
            TeamId = p.TeamId,
            Skill = p.Skill.ToString()
        }) ?? throw new NotFoundException(nameof(Player));

        return player;
    }

    public async Task UpdatePlayerAsync(UpdatePlayerRequest player)
    {
        Player existingPlayer = await _playerRepository.FindAsync(player.Id) ?? throw new NotFoundException(nameof(Player));

        existingPlayer.Name = player.Name;
        existingPlayer.DateOfBirth = player.DateOfBirth;
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
}
