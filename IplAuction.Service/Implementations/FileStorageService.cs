using IplAuction.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Implementations;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _uploadsFolder;

    public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

        // Ensure the uploads directory exists when the service is instantiated
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }

        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string?> UploadFileAsync(IFormFile file, string? uploadSubFolder, int entityId)
    {
        if (entityId != 0 && !string.IsNullOrEmpty(uploadSubFolder))
        {
            var uniqueFileName = $"{entityId}{Path.GetExtension(file.FileName)}";
            var uploadPath = Path.Combine(_uploadsFolder, uploadSubFolder);
            var originalFilePath = Path.Combine(uploadPath, uniqueFileName);

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
            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{uploadSubFolder}/{uniqueFileName}";
            return fileUrl;
        }
        else
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
            var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{uploadSubFolder}/{uniqueFileName}";
            return fileUrl;
        }
    }
}
