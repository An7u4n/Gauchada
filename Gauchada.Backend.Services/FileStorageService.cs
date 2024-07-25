using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;

public class FileStorageService() : IFileStorageService
{
    public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions, string userType)
    {
        if (imageFile == null)
        {
            throw new ArgumentNullException(nameof(imageFile));
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (userType == "driver")
        {
            path = Path.Combine(path, "driver");
        }
        else if (userType == "passenger")
        {
            path = Path.Combine(path, "passenger");
        }
        else throw new Exception("Invalid user type");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // Check the allowed extenstions
        var ext = Path.GetExtension(imageFile.FileName);
        if (!allowedFileExtensions.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
        }
            
        // generate a unique filename
        var fileName = $"{Guid.NewGuid().ToString()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        return fileName;
    }
}