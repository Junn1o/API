using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GellAllAuthors();
        AuthorDTO GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthor);
        AddAuthorRequestDTO UpdateAuthorById(int id, AddAuthorRequestDTO updateAuthor);
        Author? DeleteAuthorById(int id);
    }
}
