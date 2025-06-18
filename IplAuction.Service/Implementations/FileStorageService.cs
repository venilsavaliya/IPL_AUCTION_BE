using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IplAuction.Service.Implementations;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FileStorageService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _uploadsFolder;

    public FileStorageService(IWebHostEnvironment env, ILogger<FileStorageService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _logger = logger;
        _uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

        // Ensure the uploads directory exists when the service is instantiated
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }

        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string?> UploadFileAsync(IFormFile file)
    {
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var originalFilePath = Path.Combine(_uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(originalFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
        {
            return null; // Cannot construct a full URL without HttpContext
        }

        // Construct the full URL for the original image
        var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{uniqueFileName}";
        return fileUrl;
    }
}
