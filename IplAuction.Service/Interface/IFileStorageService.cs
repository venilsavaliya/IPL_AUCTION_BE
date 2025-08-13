using Microsoft.AspNetCore.Http;

namespace IplAuction.Service.Interface;

public interface IFileStorageService
{
    Task<string?> UploadFileAsync(IFormFile file,string? uploadSubFolder,int entityId);
}
