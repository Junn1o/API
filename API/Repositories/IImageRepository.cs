using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IImageRepository
    {
        string SaveImage(IFormFile imageFile);
    }
}