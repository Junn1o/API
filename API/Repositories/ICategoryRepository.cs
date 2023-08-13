using System.Collections.Generic;
using API.Models.DTO;

namespace API.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategory();
        CategorywithIdDTO GetCategoryById(int id);
        AddCategoryRequestDTO AddCategory(AddCategoryRequestDTO addCategory);
        AddCategoryRequestDTO UpdateCategoryById(int id, AddCategoryRequestDTO updateCategory);
        CategoryDTO? DeleteCategoryById(int id);
    }
}
