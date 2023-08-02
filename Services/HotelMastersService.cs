using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class HotelMastersService: IHotelMastersService
    {
        private readonly ICrud<HotelMaster, IdDTO> _HotelMasterRepo;

        public HotelMastersService(ICrud<HotelMaster, IdDTO> HotelMasterRepo)
        {
            _HotelMasterRepo = HotelMasterRepo;
        }

        public async Task<HotelMaster> Add_HotelMaster(HotelMaster hotelMaster)
        {
            var HotelMasters = await _HotelMasterRepo.GetAll();
            var newHotelMaster = HotelMasters.SingleOrDefault(h => h.Id == hotelMaster.Id);
            if (newHotelMaster == null)
            {
                var myhotel = await _HotelMasterRepo.Add(hotelMaster);
                if (myhotel != null)
                    return myhotel;
            }
            return null;
        }

        public async Task<List<HotelMaster>?> View_All_HotelMaster()
        {
            var myhotel = await _HotelMasterRepo.GetAll();
            return myhotel;
        }
    }
}
