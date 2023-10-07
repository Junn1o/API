using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace API.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLAuthorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<AuthorDTO> GellAllAuthors()
        {
            var authorlist = _appDbContext.Author.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                fullname = author.firstname,
                address = author.address,
                FormattedBirthday = author.birthday.ToString("dd/MM/yyyy"),
                FormattedDatecreated = author.datecreated.ToString("dd/MM/yyyy"),
                password = author.password,
                phone = author.phone,
                actualFile = author.actualFile,
            }).ToList();
            return authorlist;
        }
        public AuthorwithIdDTO GetAuthorById(int id)
        {
            var getAuthorbyDomain = _appDbContext.Author.Where(cd => cd.Id == id);
            var getCategorybyDTO = getAuthorbyDomain.Select(author => new AuthorwithIdDTO()
            {
                firstname = author.firstname,
                lastname = author.lastname,
                address = author.address,
                FormattedBirthday = author.birthday.ToString("dd/MM/yyyy"),
                FormattedDatecreated = author.datecreated.ToString("dd/MM/yyyy"),
                password = author.password,
                phone = author.phone,
                actualFile = author.actualFile,
                roomlist = author.room.Select(r=>r.title).ToList()
            }).FirstOrDefault();
            return getCategorybyDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthor)
        {
            if (addAuthor.FileUri != null)
            {
                addAuthor.birthday = DateTime.ParseExact(addAuthor.FormattedBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var authorDomain = new Author
                {
                    firstname = addAuthor.firstname,
                    lastname = addAuthor.lastname,
                    address = addAuthor.address,
                    gender = addAuthor.gender,
                    birthday = addAuthor.birthday,
                    password = addAuthor.password,
                    phone = addAuthor.phone,
                    datecreated = DateTime.Now,
                    FileUri = addAuthor.FileUri,
                };
                addAuthor.actualFile = UploadImage(addAuthor.FileUri, authorDomain.Id, authorDomain.datecreated.ToString("yyyy"));
                authorDomain.actualFile = addAuthor.actualFile;
                _appDbContext.Author.Add(authorDomain);
                _appDbContext.SaveChanges();
            }
            return addAuthor;
        }
        public AddAuthorRequestDTO UpdateAuthorById(int id, AddAuthorRequestDTO updateAuthor)
        {
            var authorDomain = _appDbContext.Author.FirstOrDefault(cd => cd.Id == id);
            if (authorDomain != null)
            {
                updateAuthor.birthday = DateTime.ParseExact(updateAuthor.FormattedBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (UpdateImage(updateAuthor.FileUri, authorDomain.actualFile, id, authorDomain.datecreated.ToString("yyyy")) == null)
                {
                    updateAuthor.actualFile = UploadImage(updateAuthor.FileUri, id, updateAuthor.datecreated.ToString("yyyy"));
                }
                else
                {
                    updateAuthor.actualFile = UpdateImage(updateAuthor.FileUri, authorDomain.actualFile, id, authorDomain.datecreated.ToString("yyyy"));
                }
                authorDomain.firstname = updateAuthor.firstname;
                authorDomain.lastname = updateAuthor.lastname;
                authorDomain.address = updateAuthor.address;
                authorDomain.gender = updateAuthor.gender;
                authorDomain.birthday = updateAuthor.birthday;
                authorDomain.password = updateAuthor.password;
                authorDomain.phone = updateAuthor.phone;
                authorDomain.FileUri = updateAuthor.FileUri;
                authorDomain.actualFile = updateAuthor.actualFile;
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
                if (DeleteImage(authorDomain.actualFile) == true)
                {
                    DeleteImage(authorDomain.actualFile);
                }
                if (DeleteImage(authorDomain.actualFile) == false)
                {
                    return null;
                }
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
        public string UploadImage(IFormFile file, int id, string datecreated)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "author", id + "-" + datecreated);
            Directory.CreateDirectory(uploadFolderPath);
            var filePath = Path.Combine(uploadFolderPath, "avatar" + fileExtension);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(ms);
            }
            var path = Path.Combine("images", "author", id + "-" + datecreated, "avatar" + fileExtension);
            return path;
        }
        public string UpdateImage(IFormFile file, string currentpath, int id, string datecreated)
        {
            if (currentpath != null)
            {
                var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", currentpath);
                if (!File.Exists(oldFullPath))
                {
                    return null;
                }
                else
                {
                    //var oldDirectory = Path.GetDirectoryName(oldFullPath);
                    //throw new Exception(oldDirectory);
                    File.Delete(oldFullPath);
                    var newPath = UploadImage(file, id, datecreated);
                    return newPath;
                }
            }
            else
            {
                return null;
            }
        }
        public bool DeleteImage(string imagePath)
        {
            //string fileName = Path.GetFileName(oldRelativePath);
            string parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(imagePath));
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "author", parentDirectoryName);
            //!File.Exists(filePath)
            if (!Directory.Exists(folderPath))
            {
                return false;
            }
            else
            {
                //File.Delete(filePath);
                Directory.Delete(folderPath, true);
                return true;
            }
        }
    }
}
