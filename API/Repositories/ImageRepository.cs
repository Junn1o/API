using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly string _uploadsPath;

        public ImageRepository(IWebHostEnvironment hostingEnvironment)
        {
            _uploadsPath = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
        }

        public string SaveImage(IFormFile imageFile)
        {
            if (imageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(_uploadsPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                return "/uploads/" + uniqueFileName;
            }

            return null;
        }
    }
}
