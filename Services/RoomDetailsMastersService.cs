using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class RoomDetailsMastersService: IRoomDetailsMastersService
    {
        private readonly ICrud<RoomDetailsMaster, IdDTO> _roomDetailsMasterrepo;

        public RoomDetailsMastersService(ICrud<RoomDetailsMaster, IdDTO> roomDetailsMasterrepo)
        {
            _roomDetailsMasterrepo = roomDetailsMasterrepo;
        }

        public async Task<RoomDetailsMaster> Add_RoomDetails(RoomDetailsMaster roomDetailsMaster)
        {
            var roomdetails = await _roomDetailsMasterrepo.GetAll();
            var newroomdetails = roomdetails.SingleOrDefault(h => h.Id == roomDetailsMaster.Id);
            if (newroomdetails == null)
            {
                var mypackage = await _roomDetailsMasterrepo.Add(roomDetailsMaster);
                if (mypackage != null)
                    return mypackage;
            }
            return null;
        }

        public async Task<List<RoomDetailsMaster>?> View_All_RoomDetails()
        {
            var roomdetails = await _roomDetailsMasterrepo.GetAll();
            return roomdetails;
        }
    }
}
