using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IRoomDetailsMastersService
    {
        Task<RoomDetailsMaster> Add_RoomDetails(RoomDetailsMaster roomDetailsMaster);
        Task<List<RoomDetailsMaster>?> View_All_RoomDetails();
    }
}
