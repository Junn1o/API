
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace API.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<UserDTO> GetAllUser()
        {
            var userlist = _appDbContext.User.Select(User => new UserDTO()
            {
                Id = User.Id,
                firstname = User.firstname,
                lastname = User.lastname,
                address = User.address,
                FormattedBirthday = User.birthday.ToString("dd/MM/yyyy"),
                FormattedDatecreated = User.datecreated.ToString("dd/MM/yyyy"),
                password = User.password,
                phone = User.phone,
                actualFile = User.actualFile,
            }).ToList();
            return userlist;
        }
        public UserwithIdDTO GetUserById(int id)
        {
            var getUserbyDomain = _appDbContext.User.Where(rd => rd.Id == id);
            var getUserbyDTO = getUserbyDomain.Select(User => new UserwithIdDTO()
            {
                firstname = User.firstname,
                lastname = User.lastname,
                address = User.address,
                FormattedBirthday = User.birthday.ToString("dd/MM/yyyy"),
                FormattedDatecreated = User.datecreated.ToString("dd/MM/yyyy"),
                password = User.password,
                phone = User.phone,
                actualFile = User.actualFile,
            }).FirstOrDefault();
            return getUserbyDTO;
        }
        public AddUserRequestDTO AddUser(AddUserRequestDTO addUser)
        {
            if (addUser.FileUri != null)
            {
                addUser.birthday = DateTime.ParseExact(addUser.FormattedBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                addUser.datecreated = DateTime.Now;
                var userDomain = new User
                {
                    firstname = addUser.firstname,
                    lastname = addUser.lastname,
                    address = addUser.address,
                    gender = addUser.gender,
                    datecreated = addUser.datecreated,
                    birthday = addUser.birthday,
                    password = addUser.password,
                    phone = addUser.phone,
                    FileUri = addUser.FileUri,
                };
                addUser.actualFile = UploadImage(addUser.FileUri, userDomain.Id, addUser.datecreated.ToString("yyyy"));
                userDomain.actualFile = addUser.actualFile;
                _appDbContext.User.Add(userDomain);
                _appDbContext.SaveChanges();
            }
            return addUser;
        }
        public AddUserRequestDTO? UpdateUserById(int id, AddUserRequestDTO updateUser)
        {
            var userDomain = _appDbContext.User.FirstOrDefault(u => u.Id == id);
            if (userDomain != null)
            {
                updateUser.birthday = DateTime.ParseExact(updateUser.FormattedBirthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (UpdateImage(updateUser.FileUri, userDomain.actualFile, id, userDomain.datecreated.ToString("yyyy")) == null)
                {
                    updateUser.actualFile = UploadImage(updateUser.FileUri, id, userDomain.datecreated.ToString("yyyy"));
                }
                else
                {
                    updateUser.actualFile = UpdateImage(updateUser.FileUri, userDomain.actualFile, id, userDomain.datecreated.ToString("yyyy"));
                }
                userDomain.firstname = updateUser.firstname;
                userDomain.lastname = updateUser.lastname;
                userDomain.address = updateUser.address;
                userDomain.gender = updateUser.gender;
                userDomain.birthday = updateUser.birthday;
                userDomain.password = updateUser.password;
                userDomain.phone = updateUser.phone;
                userDomain.FileUri = updateUser.FileUri;
                userDomain.actualFile = updateUser.actualFile;
                _appDbContext.SaveChanges();
            }
            return updateUser;
        }
        public User? DeleteUserById(int id)
        {
            var userDomain = _appDbContext.User.FirstOrDefault(r => r.Id == id);
            if (userDomain != null)
            {
                if (DeleteImage(userDomain.actualFile) == true)
                {
                    DeleteImage(userDomain.actualFile);
                }
                _appDbContext.User.Remove(userDomain);
                _appDbContext.SaveChanges();
                return userDomain;
            }
            else
            {
                return null;
            }
        }
        //function to handle image
        public string UploadImage(IFormFile file, int id, string datecreated)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", id + "-" + datecreated);
            Directory.CreateDirectory(uploadFolderPath);
            var filePath = Path.Combine(uploadFolderPath, "avatar" + fileExtension);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(ms);
            }
            var path = Path.Combine("images", "user", id + "-" + datecreated, "avatar" + fileExtension);
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
            string parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(imagePath));
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", parentDirectoryName);
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
