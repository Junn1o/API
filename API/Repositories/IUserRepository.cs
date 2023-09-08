using System.Collections.Generic;
using API.Models.Domain;
using API.Models.DTO;

namespace API.Repositories
{
    public interface IUserRepository
    {
        List<UserDTO> GetAllUser();
        UserwithIdDTO GetUserById(int id);
        AddUserRequestDTO AddUser(AddUserRequestDTO addUser);
        AddUserRequestDTO? UpdateUserById(int id, AddUserRequestDTO updateUser);
        User? DeleteUserById(int id);
    }
}
