using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class RoomTypeMastersService: IRoomTypeMastersService
    {
        private readonly ICrud<RoomTypeMaster, IdDTO> _roomTypeMasterRepo;

        public RoomTypeMastersService(ICrud<RoomTypeMaster, IdDTO> roomTypeMasterRepo)
        {
            _roomTypeMasterRepo = roomTypeMasterRepo;   
        }

        public async Task<RoomTypeMaster> Add_RoomType(RoomTypeMaster roomTypeMaster)
        {
            var roomType = await _roomTypeMasterRepo.GetAll();
            var newroomdetails = roomType.SingleOrDefault(h => h.Id == roomTypeMaster.Id);
            if (newroomdetails == null)
            {
                var myRoomType = await _roomTypeMasterRepo.Add(roomTypeMaster);
                if (myRoomType != null)
                    return myRoomType;
            }
            return null;
        }

        public async Task<List<RoomTypeMaster>?> View_All_RoomType()
        {
            var RoomTypeMasters = await _roomTypeMasterRepo.GetAll();
            return RoomTypeMasters;
        }
    }
}
