using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourTrip.Services
{
    public class HotelMastersService: IHotelMastersService
    {
        private readonly ICrud<HotelMaster, IdDTO> _HotelMasterRepo;
        private readonly IImageRepo<HotelMaster, HotelFormModule> _imageRepo;

        public HotelMastersService(ICrud<HotelMaster, IdDTO> HotelMasterRepo, IImageRepo<HotelMaster, HotelFormModule> imageRepo)
        {
            _HotelMasterRepo = HotelMasterRepo;
            _imageRepo = imageRepo;

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
        public async Task<HotelMaster?> View_HotelMaster(IdDTO idDTO)
        {
            var HotelMaster = await _HotelMasterRepo.GetValue(idDTO);
            return HotelMaster;
        }

        public async Task<HotelMaster> PostImage([FromForm] HotelFormModule hotelFormModule)
        {
            if (hotelFormModule == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _imageRepo.PostImage(hotelFormModule);
            if (item == null)
            {
                return null;
            }
            return item;
        }
    }
}
