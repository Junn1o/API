using System.Collections.Generic;
using API.Models.DTO;

namespace API.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategory();
        CategorywithIdDTO GetCategoryById(int id);
        AddCategoryRequestDTO AddCategory(AddCategoryRequestDTO addCategoryRequestDTO);
        CategorywithIdDTO UpdateCategoryById(int id, CategorywithIdDTO categoryNoIdDTO);
        CategoryDTO? DeleteCategoryById(int id);
    }
}
