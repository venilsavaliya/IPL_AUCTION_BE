using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;

namespace IplAuction.Service.Implementations;

public class PlayerImportService(IPlayerRepository playerRepository, ITeamService teamService, IPlayerService playerService) : IPlayerImportService
{
    private readonly IPlayerRepository _playerRepository = playerRepository;

    private readonly IPlayerService _playerService = playerService;

    private readonly ITeamService _teamService = teamService;

    public async Task<CsvImportResult> ProcessCsvAsync(StreamReader reader)
    {
        var result = new CsvImportResult();
        var playersToAdd = new List<Player>();
        Dictionary<string, int> teams = _teamService.GetTeamNameIdDictionary();
        Dictionary<string, int> players = _playerService.GetPlayerNameIdDictionary();


        // Step 1: Read headers
        var headerLine = await reader.ReadLineAsync();
        if (string.IsNullOrWhiteSpace(headerLine))
        {
            result.Errors.Add("CSV is empty or missing header row.");
            return result;
        }

        var headers = headerLine.Split(',').Select(h => h.Trim().ToLower()).ToList();

        // Helper to get index by column name
        int GetIndex(string columnName) => headers.IndexOf(columnName.ToLower());

        int rowNumber = 2; // Starting from 2 since header is row 1

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var columns = line.Split(',');

            string GetColumnValue(string columnName)
            {
                int index = GetIndex(columnName);
                return (index >= 0 && index < columns.Length) ? columns[index].Trim() : "";
            }

            string name = GetColumnValue("name");
            string skillStr = GetColumnValue("skill");
            string teamName = GetColumnValue("team").ToLower();
            string dobStr = GetColumnValue("dateofbirth");
            string country = GetColumnValue("country");
            string basePriceStr = GetColumnValue("baseprice");

            // Validation
            if (string.IsNullOrWhiteSpace(name))
                result.Errors.Add($"Row {rowNumber}: Name is required.");

            if (string.IsNullOrWhiteSpace(country))
                result.Errors.Add($"Row {rowNumber}: Country is required.");

            if (!Enum.TryParse<PlayerSkill>(skillStr, out var skill))
                result.Errors.Add($"Row {rowNumber}: Invalid skill '{skillStr}'.");

            var teamId = teams.GetValueOrDefault(teamName);
            if (teamId == 0)
                result.Errors.Add($"Row {rowNumber}: Team '{teamName}' not found.");

            DateOnly? dob = null;
            if (!string.IsNullOrWhiteSpace(dobStr) && !DateOnly.TryParse(dobStr, out var parsedDob))
                result.Errors.Add($"Row {rowNumber}: Invalid date of birth '{dobStr}'.");
            else if (!string.IsNullOrWhiteSpace(dobStr))
                dob = DateOnly.Parse(dobStr);

            if (!decimal.TryParse(basePriceStr, out var basePrice))
                result.Errors.Add($"Row {rowNumber}: Invalid base price '{basePriceStr}'.");

            // Add only if no errors for this row
            if (!result.Errors.Any(e => e.StartsWith($"Row {rowNumber}:")))
            {
                int playerId = players.GetValueOrDefault(name.ToLower());

                if (playerId != 0)
                {
                    Player? player = await _playerRepository.GetWithFilterAsync(p => p.Id == playerId);

                    if (player == null)
                    {
                        playersToAdd.Add(new Player
                        {
                            Name = name,
                            Skill = skill,
                            TeamId = teamId,
                            DateOfBirth = dob,
                            Country = country,
                            IsActive = true,
                            BasePrice = basePrice
                        });
                    }
                    else
                    {
                        player.Name = name;
                        player.Skill = skill;
                        player.TeamId = teamId;
                        player.DateOfBirth = dob;
                        player.Country = country;
                        player.BasePrice = basePrice;

                        await _playerRepository.SaveChangesAsync();
                    }
                }
                else
                {
                    playersToAdd.Add(new Player
                    {
                        Name = name,
                        Skill = skill,
                        TeamId = teamId,
                        DateOfBirth = dob,
                        Country = country,
                        IsActive = true,
                        BasePrice = basePrice
                    });
                }
                result.SuccessfulInserts++;
            }
            rowNumber++;
        }

        result.TotalRows = rowNumber - 2;

        if (result.Errors.Count == 0)
        {
            await _playerRepository.AddRangeAsync(playersToAdd);
            await _playerRepository.SaveChangesAsync();
        }
        return result;
    }

    public List<CsvValidationError> ValidateCsvRows(List<PlayerCsvModel> players)
    {
        var errors = new List<CsvValidationError>();

        for (int i = 0; i < players.Count; i++)
        {
            var p = players[i];
            int rowNum = i + 2; // +2 assuming header is row 1

            if (string.IsNullOrWhiteSpace(p.Name))
                errors.Add(new CsvValidationError { RowNumber = rowNum, FieldName = "Name", ErrorMessage = "Name is required." });

            if (string.IsNullOrWhiteSpace(p.Country))
                errors.Add(new CsvValidationError { RowNumber = rowNum, FieldName = "Country", ErrorMessage = "Country is required." });

            if (string.IsNullOrWhiteSpace(p.Role))
                errors.Add(new CsvValidationError { RowNumber = rowNum, FieldName = "Role", ErrorMessage = "Role is required." });

            if (p.BasePrice == null || p.BasePrice <= 0)
                errors.Add(new CsvValidationError { RowNumber = rowNum, FieldName = "BasePrice", ErrorMessage = "Valid BasePrice is required." });

            // You can skip DateOfBirth and ImageUrl
        }

        return errors;
    }
}

