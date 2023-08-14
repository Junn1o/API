using API.Models.Domain;
using API.Models.DTO;

namespace API.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategory();
        CategorywithIdDTO GetCategoryById(int id);
        AddCategoryRequestDTO AddCategory(AddCategoryRequestDTO addCategory);
        AddCategoryRequestDTO UpdateCategory(int id, AddCategoryRequestDTO updateCategory);
        Category? DeleteCategory(int id);
    }
}
