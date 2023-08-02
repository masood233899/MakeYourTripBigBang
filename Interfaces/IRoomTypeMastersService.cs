using MakeYourTrip.Models;

namespace MakeYourTrip.Interfaces
{
    public interface IRoomTypeMastersService
    {
        Task<RoomTypeMaster> Add_RoomType(RoomTypeMaster roomTypeMaster);
        Task<List<RoomTypeMaster>?> View_All_RoomType();
    }
}
