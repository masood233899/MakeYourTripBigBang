using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Interfaces
{
    public interface IRoomDetailsMastersService
    {
        Task<List<RoomDetailsMaster>?> Add_RoomDetailsMaster(List<RoomDetailsMaster> roomDetailsMaster);
        Task<List<RoomDetailsMaster>?> View_All_RoomDetails();
        Task<List<RoomdetailsDTO>> getRoomDetailsByHotel(IdDTO id);

    }
}
