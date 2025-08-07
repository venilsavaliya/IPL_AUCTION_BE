using IplAuction.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Interface;

public interface IPlayerImportService
{
    Task<CsvImportResult> ProcessCsvAsync(StreamReader reader);

    Task<CsvImportResult> ProcessImportAsync(IFormFile file);

    Task ReadExcelAsync(IFormFile file);
}
