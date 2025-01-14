namespace Ticket.Interfaces;
public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile imageFile);
}
