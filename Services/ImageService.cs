using Ticket.Interfaces;

namespace Ticket.Services;
public class ImageService(IWebHostEnvironment webHostEnvironment) : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new ArgumentException("Invalid image file.");
        }

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

        var uploadPath = Path.Combine("wwwroot", "upload", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)!);

        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return $"/uploads/{fileName}";
    }
}
