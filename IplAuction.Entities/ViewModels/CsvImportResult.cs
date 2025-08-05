namespace IplAuction.Entities.ViewModels;

public class CsvImportResult
{
    public List<string> Errors { get; set; } = new();
    public int TotalRows { get; set; }
    public int SuccessfulInserts { get; set; }
}