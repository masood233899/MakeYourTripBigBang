using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Interfaces
{
    public interface IUsersService
    {
        Task<UserDTO> Register(UserRegisterDTO userRegisterDTO);
        Task<UserDTO> Login(UserDTO userDTO);
        Task<UserDTO> Update(UserRegisterDTO user);
        Task<bool> UpdatePassword(UserDTO userDTO);
    }
}
