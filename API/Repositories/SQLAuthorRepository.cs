using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SQLAuthorRepository :IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLAuthorRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<AuthorDTO> GellAllAuthors()
        {
            var authorlist = _appDbContext.Author.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                fullname = author.fullname,
                password = author.password,
                phone = author.phone,
            }).ToList();
            return authorlist;
        }
        public AuthorwithIdDTO GetAuthorById(int id)
        {
            var getAuthorbyDomain = _appDbContext.Author.Where(cd => cd.Id == id);
            var getCategorybyDTO = getAuthorbyDomain.Select(author => new AuthorwithIdDTO()
            {
                fullname = author.fullname,
                password= author.password,
                phone = author.phone,
                roomlist = author.room.Select(r=>r.title).ToList()
            }).FirstOrDefault();
            return getCategorybyDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthor)
        {
            var authorDomain = new Author
            {
                fullname = addAuthor.fullname,
                password = addAuthor.password,
                phone = addAuthor.phone,
            };
            _appDbContext.Author.Add(authorDomain);
            _appDbContext.SaveChanges();
            return addAuthor;
        }
        public AddAuthorRequestDTO UpdateAuthorById(int id, AddAuthorRequestDTO updateAuthor)
        {
            var authorDomain = _appDbContext.Author.FirstOrDefault(cd => cd.Id == id);
            if (authorDomain != null)
            {
                 authorDomain.fullname = updateAuthor.fullname;
                _appDbContext.SaveChanges();
            }
            return updateAuthor;
        }
        public Author? DeleteAuthorById(int id)
        {
            var authorDomain = _appDbContext.Author.FirstOrDefault(c => c.Id == id);
            var authorRoom = _appDbContext.Room.Where(ar => ar.authorId == id).ToList();
            if (authorDomain != null)
            {
                if (authorRoom.Any())
                {
                    foreach (var room in authorRoom)
                    {
                        var authorRoomCategory = _appDbContext.Room_Category.Where(n => n.roomId == room.Id).ToList();
                        if (authorRoomCategory.Any())
                        {
                            _appDbContext.Room_Category.RemoveRange(authorRoomCategory);
                            _appDbContext.SaveChanges();
                        }
                    }
                    _appDbContext.Room.RemoveRange(authorRoom);
                    _appDbContext.SaveChanges();
                    _appDbContext.Author.Remove(authorDomain);
                    _appDbContext.SaveChanges();
                }
                else
                {
                    _appDbContext.Author.Remove(authorDomain);
                    _appDbContext.SaveChanges();
                }
            }
            return authorDomain;
        }
    }
}
