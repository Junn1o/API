using System.Collections.Generic;
using API.Models.Domain;
using API.Models.DTO;

namespace API.Repositories
{
    public interface IUserRepository
    {
        List<UserDTO> GetAllUser();
        UserDTO GetUserById(int id);
        AddUserRequestDTO AddUser(AddUserRequestDTO addUserRequestDTO);
        AddUserRequestDTO? UpdateUserById(int id, AddUserRequestDTO userDTO);
        User? DeleteUserById(int id);
    }
}
