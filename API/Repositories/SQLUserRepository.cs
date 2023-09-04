
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using System.Diagnostics;
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
                fullname = User.fullname,
                password = User.password,
                phone = User.phone, 
                actuallFile = User.actualFile,
            }).ToList();
            return userlist;
        }
        public UserDTO GetUserById(int id)
        {
            var getUserbyDomain = _appDbContext.User.Where(rd => rd.Id == id);
            var getUserbyDTO = getUserbyDomain.Select(User => new UserDTO()
            {
                Id = User.Id,
                fullname = User.fullname,
                password = User.password,
                phone = User.phone,
                actuallFile = User.actualFile,
            }).FirstOrDefault();
            return getUserbyDTO;
        }
        public AddUserRequestDTO AddUser(AddUserRequestDTO addUser)
        {
            string path="";
            if (addUser.FileUri !=null )
            {
                path = UploadImage(addUser.FileUri, addUser.fullname);
                addUser.actualFile = path;
            }
            else
            {
                path = "";
            }
            var userDomain = new User
            {
                fullname = addUser.fullname,
                password = addUser.password,
                phone = addUser.phone,
                FileUri = addUser.FileUri,
                actualFile = addUser.actualFile,
            };
            _appDbContext.User.Add(userDomain);
            _appDbContext.SaveChanges();
            return addUser;
        }
        public AddUserRequestDTO? UpdateUserById(int id, AddUserRequestDTO updateUser)
        {
            var userDomain = _appDbContext.User.FirstOrDefault(r => r.Id == id);
            if (userDomain != null)
            {
                string path = "";
                if (updateUser.FileUri != null)
                {
                    if (UpdateImage(updateUser.FileUri, userDomain.actualFile, updateUser.fullname) == null)
                    {
                        path = UploadImage(updateUser.FileUri, updateUser.fullname);
                        updateUser.actualFile = path;
                    }
                    else
                    {
                        path = UpdateImage(updateUser.FileUri, userDomain.actualFile, updateUser.fullname);
                        updateUser.actualFile = path;
                    }
                }
                else
                {
                    path = "";
                }
                userDomain.fullname = updateUser.fullname;
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
                    _appDbContext.User.Remove(userDomain);
                    _appDbContext.SaveChanges();
                    return userDomain;
                }
                if (DeleteImage(userDomain.actualFile) == false)
                {
                    _appDbContext.User.Remove(userDomain);
                    _appDbContext.SaveChanges();
                    return userDomain;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        //function to handle image
        public string UploadImage(IFormFile file, string username)
        {
            var fileName = Path.GetFileName(file.FileName);
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", username + "-" + fileName);
            Directory.CreateDirectory(uploadFolderPath);
            var filePath = Path.Combine(uploadFolderPath, fileName);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(ms);
            }
            var path = Path.Combine("images", "user", username + "-" + fileName, fileName);
            return path;
        }
        public string UpdateImage(IFormFile newImage, string oldRelativePath, string username)
        {
            if (oldRelativePath != null)
            {
                var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldRelativePath);
                if (!File.Exists(oldFullPath))
                {
                    return null;
                }
                else
                {
                    var oldDirectory = Path.GetDirectoryName(oldFullPath);
                    var newRelativePath = UploadImage(newImage, username);
                    File.Delete(oldFullPath);
                    return newRelativePath;
                }
            }
            else
            {
                return null;
            }
        }
        public bool DeleteImage(string oldRelativePath)
        {
            //string fileName = Path.GetFileName(oldRelativePath);
            string parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(oldRelativePath));
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", parentDirectoryName);
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
