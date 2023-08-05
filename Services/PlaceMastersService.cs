using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;

namespace MakeYourTrip.Services
{
    public class PlaceMastersService: IPlaceMastersService
    {
        private readonly ICrud<PlaceMaster, IdDTO> _placemaster;

        public PlaceMastersService(ICrud<PlaceMaster, IdDTO> placemaster)
        {
            _placemaster = placemaster;
        }

        public async Task<PlaceMaster> Add_PlaceMaster(PlaceMaster placeMaster)
        {
            var placeMasters = await _placemaster.GetAll();

            var newPlaceMaster = placeMasters.SingleOrDefault(h => h.Id == placeMaster.Id);
            if (newPlaceMaster == null)
            {
                var myplace = await _placemaster.Add(placeMaster);
                if (myplace != null)
                    return myplace;
            }
            return null;
        }

        public async Task<PlaceMaster?> Delete_PlaceMaster(IdDTO idDTO)
        {
            var PlaceMaster = await _placemaster.Delete(idDTO);
            return PlaceMaster;
        }


        public async Task<PlaceMaster?> Update_PlaceMaster(PlaceMaster placeMaster)
        {
            var myPlateSize = await _placemaster.Update(placeMaster);
            return myPlateSize;
        }

        public async Task<List<PlaceMaster>?> View_All_PlaceMasters()
        {
            var myplace = await _placemaster.GetAll();
            return myplace;
        }

        public async Task<PlaceMaster?> View_PlaceMaster(IdDTO idDTO)
        {
            var PlaceMaster = await _placemaster.GetValue(idDTO);
            return PlaceMaster;
        }
    }
}
