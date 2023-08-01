using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;

namespace MakeYourTrip.Services
{
    public class PlaceMastersService: IPlaceMastersService
    {
        private readonly ICrud<PlaceMaster, IdDTO> _placemaster;

        public PlaceMastersService(ICrud<PlaceMaster, IdDTO> placemaster)
        {
            _placemaster = placemaster;
        }

        public Task<PlaceMaster> Add_PlaceMaster(PlaceMaster placeMaster)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceMaster?> Delete_PlaceMaster(IdDTO idDTO)
        {
            throw new NotImplementedException();
        }


        public Task<PlaceMaster?> Update_PlaceMaster(PlaceMaster placeMaster)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlaceMaster>?> View_All_PlaceMasters()
        {
            throw new NotImplementedException();
        }

        public Task<PlaceMaster?> View_PlaceMaster(IdDTO idDTO)
        {
            throw new NotImplementedException();
        }
    }
}
