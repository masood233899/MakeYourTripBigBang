using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Interfaces
{
    public interface IUsersService
    {
        Task<UserDTO> Register(UserRegisterDTO userRegisterDTO);
        Task<UserDTO> Login(UserDTO userDTO);
        Task<UserDTO> Update(UserRegisterDTO user);
        Task<bool> UpdatePassword(UserDTO userDTO);
        Task<User?> ApproveAgent(User agent);
        Task<List<User>?> GetUnApprovedAgent();
        Task<User?> DeleteAgent(UserDTO user);
    }
}
