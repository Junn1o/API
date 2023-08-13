using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GellAllAuthors();
        AuthorwithIdDTO GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AuthorwithIdDTO UpdateAuthorById(int id, AuthorwithIdDTO authorNoIdDTO);
        Author? DeleteAuthorById(int id);
    }
}
