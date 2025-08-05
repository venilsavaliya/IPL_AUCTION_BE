using IplAuction.Entities.ViewModels;

namespace IplAuction.Service.Interface;

public interface IPlayerImportService
{
    Task<CsvImportResult> ProcessCsvAsync(StreamReader reader);
}
