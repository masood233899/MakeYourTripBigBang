using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Interfaces
{
    public interface ITokenGenerate
    {
        public string GenerateToken(UserDTO user);
    }
}
