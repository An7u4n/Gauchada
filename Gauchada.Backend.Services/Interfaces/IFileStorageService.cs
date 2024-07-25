using Microsoft.AspNetCore.Http;

namespace Gauchada.Backend.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string userType);
    }
}
