using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class RoomDetailsMastersService: IRoomDetailsMastersService
    {
        private readonly ICrud<RoomDetailsMaster, IdDTO> _roomDetailsMasterrepo;
        private readonly ICrud<RoomTypeMaster, IdDTO> _RoomTypeMasterRepo;
        private readonly ICrud<HotelMaster, IdDTO> _hotelMasterRepo;

        public RoomDetailsMastersService(ICrud<RoomDetailsMaster, IdDTO> roomDetailsMasterrepo,
                        ICrud<RoomTypeMaster, IdDTO> RoomTypeMasterRepo, ICrud<HotelMaster, IdDTO> hotelMasterRepo)
        {
            _roomDetailsMasterrepo = roomDetailsMasterrepo;
            _RoomTypeMasterRepo = RoomTypeMasterRepo;
            _hotelMasterRepo = hotelMasterRepo;
        }

        public async Task<List<RoomDetailsMaster>?> Add_RoomDetailsMaster(List<RoomDetailsMaster> RoomDetailsMaster)
        {

            List<RoomDetailsMaster> addedRoomDetailsMaster = new List<RoomDetailsMaster>();

            var RoomDetailsMasters = await _roomDetailsMasterrepo.GetAll();

            foreach (var roomDetailsMaster in RoomDetailsMaster)
            {

                Console.WriteLine(roomDetailsMaster);

                var myRoomDetailsMaster = await _roomDetailsMasterrepo.Add(roomDetailsMaster);

                if (myRoomDetailsMaster != null)
                {
                    addedRoomDetailsMaster.Add(myRoomDetailsMaster);
                }

            }
            return addedRoomDetailsMaster;

        }

        public async Task<List<RoomDetailsMaster>?> View_All_RoomDetails()
        {
            var roomdetails = await _roomDetailsMasterrepo.GetAll();
            return roomdetails;
        }
        public async Task<List<RoomdetailsDTO>> getRoomDetailsByHotel(IdDTO id)
        {
            var RoomDetailsMasters = await _roomDetailsMasterrepo.GetAll();
            var RoomTypeMasters = await _RoomTypeMasterRepo.GetAll();
            var HotelMasters = await _hotelMasterRepo.GetAll();

            var query = (from hm in HotelMasters
                         join rd in RoomDetailsMasters on hm.Id equals rd.HotelId
                         join rt in RoomTypeMasters on rd.RoomTypeId equals rt.Id
                         where hm.Id == id.Idint
                         select new RoomdetailsDTO
                         {
                             RoomDetailsMasterId = rd.Id,
                             Price = rd.Price,
                             Description = rd.Description,
                             RoomType = rt.RoomType,

                         }).ToList();
            return query;
        }
    }
}
