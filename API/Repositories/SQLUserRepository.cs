
using API.Data;
using API.Models.Domain;
using API.Models.DTO;

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
               
            }).FirstOrDefault();
            return getUserbyDTO;
        }
        public AddUserRequestDTO AddUser(AddUserRequestDTO addUser)
        {
            var userDomain = new User
            {
                fullname = addUser.fullname,
                password = addUser.password,
                phone = addUser.phone,
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
                userDomain.fullname = updateUser.fullname;
                userDomain.password = updateUser.password;
                userDomain.phone = updateUser.phone;
                _appDbContext.SaveChanges();
            }
            return updateUser;
        }
        public User? DeleteUserById(int id)
        {
            var userDomain = _appDbContext.User.FirstOrDefault(r => r.Id == id);
            if (userDomain != null)
            {
               
                _appDbContext.User.Remove(userDomain);
                _appDbContext.SaveChanges();
            }
            return userDomain;
        }
    }
}
