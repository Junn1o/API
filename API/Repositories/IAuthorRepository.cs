using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GellAllAuthors();
        AuthorwithIdDTO GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthor);
        AuthorwithIdDTO UpdateAuthorById(int id, AuthorwithIdDTO updateAuthor);
        Author? DeleteAuthorById(int id);
    }
}
