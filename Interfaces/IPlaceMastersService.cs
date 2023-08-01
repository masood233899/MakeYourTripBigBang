using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Interfaces
{
    public interface IPlaceMastersService
    {
        Task<PlaceMaster> Add_PlaceMaster(PlaceMaster placeMaster);
        Task<PlaceMaster?> Delete_PlaceMaster(IdDTO idDTO);
        Task<PlaceMaster?> Update_PlaceMaster(PlaceMaster placeMaster);
        Task<PlaceMaster?> View_PlaceMaster(IdDTO idDTO);
        Task<List<PlaceMaster>?> View_All_PlaceMasters();
    }
}
