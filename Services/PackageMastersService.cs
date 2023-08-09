using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MakeYourTrip.Services
{
    public class PackageMastersService: IPackageMastersService
    {
        private readonly ICrud<PackageMaster, IdDTO> _packageMasterRepo;
        private readonly ICrud<PlaceMaster, IdDTO> _placemasterRepo;
        private readonly ICrud<PackageDetailsMaster, IdDTO> _packageDetailsMasterRepo;
        private readonly ICrud<HotelMaster, IdDTO> _hotelMasterRepo;
        private readonly ICrud<VehicleMaster, IdDTO> _vehicleMasterRepo;
        private readonly ICrud<VehicleDetailsMaster, IdDTO> _vehicleDetailsMasterRepo;
        private readonly IImageRepo<PackageMaster, PackageFormModel> _imageRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PackageMastersService(ICrud<PackageMaster, IdDTO> PackageMasterRepo, ICrud<PlaceMaster, IdDTO> placemasterRepo, ICrud<PackageDetailsMaster, IdDTO> packageDetailsMasterRepo
            , ICrud<HotelMaster, IdDTO> hotelMasterRepo, ICrud<VehicleMaster, IdDTO> vehicleMasterRepo,
            ICrud<VehicleDetailsMaster, IdDTO> vehicleDetailsMasterRepo,
            IImageRepo<PackageMaster, PackageFormModel> imageRepo, IWebHostEnvironment hostEnvironment)
        {
            _packageMasterRepo = PackageMasterRepo;
            _placemasterRepo = placemasterRepo;
            _packageDetailsMasterRepo = packageDetailsMasterRepo;
            _hotelMasterRepo = hotelMasterRepo;
            _vehicleMasterRepo = vehicleMasterRepo;
            _vehicleDetailsMasterRepo = vehicleDetailsMasterRepo;
            _imageRepo = imageRepo;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<PackageMaster> Add_PackageMaster(PackageMaster packageMaster)
        {
            var packages = await _packageMasterRepo.GetAll();
            var newpackage = packages.SingleOrDefault(h => h.Id == packageMaster.Id);
            if (newpackage == null)
            {
                var mypackage = await _packageMasterRepo.Add(packageMaster);
                if (mypackage != null)
                    return mypackage;
            }
            return null;
        }

        public async Task<List<PackageMaster>?> View_All_PackageMaster()
        {
            var PackageMasters = await _packageMasterRepo.GetAll();
            var images = await _packageMasterRepo.GetAll();
            var imageList = new List<PackageMaster>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, image.PackageImages);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var tourData = new PackageMaster
                {
                    Id = image.Id,
                    PackagePrice = image.PackagePrice,
                    PackageName = image.PackageName,
                    TravelAgentId = image.TravelAgentId,
                    Region = image.Region,
                    Daysno = image.Daysno,


                    PackageImages = Convert.ToBase64String(imageBytes)
                };
                imageList.Add(tourData);
            }
            return imageList;
        }
                
        public async Task<PackageMaster?> View_PackageMaster(IdDTO idDTO)
        {
            var packageMaster = await _packageMasterRepo.GetValue(idDTO);
            return packageMaster;
        }

        public async Task<PackageMaster> PostDashboardImage([FromForm] PackageFormModel packageFormModel)
        {
            if (packageFormModel == null)
            {
                throw new Exception("Invalid file");
            }
            var item = await _imageRepo.PostImage(packageFormModel);
            if (item == null)
            {
                return null;
            }
            return item;
        }



        /* public async Task<PackageDTO?> Get_package_details(IdDTO id)
         {
             var placeData = await _placemasterRepo.GetAll();
             var packageMasterData = await _packageMasterRepo.GetAll();
             var packageDetailsData = await _packageDetailsMasterRepo.GetAll();
             var hotelMasterData = await _hotelMasterRepo.GetAll();
             var vehicleMasterData = await _vehicleMasterRepo.GetAll();
             var VehicleDetailsData = await _vehicleDetailsMasterRepo.GetAll();

             var result = (from pm in packageMasterData
                           where pm.Id == id.IdInt

                           select new PackageDTO
                           {
                               PackageName = pm.PackageName,
                               PackagePrice = pm.PackagePrice,
                               TravelAgentId = pm.TravelAgentId,
                               Region = pm.Region,
                               Imagepath= pm.Imagepath,




                               placeList = (from pm1 in packageMasterData
                                          join pd in packageDetailsData on pm1.Id equals pd.PackageId
                                          join pl in placeData on pd.PlaceId equals pl.Id
                                          where pm1.Id == id.IdInt
                                           select new PlaceDTO
                                          {
                                               placeId=pl.Id,
                                              PlaceName = pl.PlaceName,
                                              DayNumber = pd.DayNumber,
                                              HotelList = (from hm in hotelMasterData
                                                           where hm.PlaceId == pl.Id
                                                           select new HotelDTO
                                                          {
                                                              HotelId= hm.Id,
                                                              HotelName =hm.HotelName,
                                                          }).ToList(),

                              VechileList= (from pm in packageMasterData
                                           join pd in packageDetailsData on pm.Id equals pd.PackageId
                                           join pl in placeData on pd.PlaceId equals pl.Id
                                           join vd in VehicleDetailsData on pl.Id equals vd.PlaceId
                                           join vm in vehicleMasterData on vd.VehicleId equals vm.Id
                                            where pm.Id == id.IdInt
                                                select new VehicleDTO
                                                {
                                                  VehicleDetailsId= vd.VehicleId,
                                                   CarPrice =vd.CarPrice,
                                                   VehicleName = vm.VehicleName,
                                                   NumberOfSeats = vm.NumberOfSeats,
                                                }).ToList(),
                                          }).ToList(),



                          }).FirstOrDefault();

             if (result != null && !string.IsNullOrEmpty(result.Imagepath))
             {
                 var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                 var filePath = Path.Combine(uploadsFolder, result.Imagepath);

                 // Check if the image file exists
                 if (File.Exists(filePath))
                 {
                     var imageBytes = await File.ReadAllBytesAsync(filePath);
                     result.Imagepath = Convert.ToBase64String(imageBytes);
                     Console.WriteLine(result.Imagepath);
                 }
             }
             return result;

         }*/
        /*public async Task<PackageDTO?> Get_package_details(IdDTO id)
        {
            var placeData = await _placemasterRepo.GetAll();
            var packageMasterData = await _packageMasterRepo.GetAll();
            var packageDetailsData = await _packageDetailsMasterRepo.GetAll();
            var hotelMasterData = await _hotelMasterRepo.GetAll();
            var vehicleMasterData = await _vehicleMasterRepo.GetAll();
            var VehicleDetailsData = await _vehicleDetailsMasterRepo.GetAll();

            var places = (from pd in packageDetailsData
                          join pl in placeData on pd.PlaceId equals pl.Id
                          where pd.PackageId == id.IdInt
                          select pl).Distinct().ToList();

            List<VehicleDTO> VechileList = new List<VehicleDTO>();

            foreach (var place in places)
            {
                var vechilesForPlace = (from vd in VehicleDetailsData
                                        join vm in vehicleMasterData on vd.VehicleId equals vm.Id
                                        where vd.PlaceId == place.Id
                                        select new VehicleDTO
                                        {
                                            VehicleDetailsId = vd.VehicleId,
                                            CarPrice = vd.CarPrice,
                                            VehicleName = vm.VehicleName,
                                            NumberOfSeats = vm.NumberOfSeats,
                                        }).ToList();

                VechileList.AddRange(vechilesForPlace);
            }

            var result = (from pm in packageMasterData
                          where pm.Id == id.IdInt
                          select new PackageDTO
                          {
                              PackageName = pm.PackageName,
                              PackagePrice = pm.PackagePrice,
                              TravelAgentId = pm.TravelAgentId,
                              Region = pm.Region,
                              Imagepath = pm.Imagepath,
                              placeList = (from pm1 in packageMasterData
                                           join pd in packageDetailsData on pm1.Id equals pd.PackageId
                                           join pl in placeData on pd.PlaceId equals pl.Id
                                           where pm1.Id == id.IdInt
                                           select new PlaceDTO
                                           {
                                               placeId = pl.Id,
                                               PlaceName = pl.PlaceName,
                                               DayNumber = pd.DayNumber,
                                               HotelList = (from hm in hotelMasterData
                                                            where hm.PlaceId == pl.Id
                                                            select new HotelDTO
                                                            {
                                                                HotelId = hm.Id,
                                                                HotelName = hm.HotelName,
                                                            }).ToList(),
                                               VechileList = VechileList // Assign the already fetched VechileList
                                           }).ToList(),
                          }).FirstOrDefault();

            if (result != null && !string.IsNullOrEmpty(result.Imagepath))
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, result.Imagepath);

                // Check if the image file exists
                if (File.Exists(filePath))
                {
                    var imageBytes = await File.ReadAllBytesAsync(filePath);
                    result.Imagepath = Convert.ToBase64String(imageBytes);
                }
            }

            return result;
        }*/

        /*public async Task<PackageDTO?> Get_package_details(IdDTO id)
        {
            var placeData = await _placemasterRepo.GetAll();
            var packageMasterData = await _packageMasterRepo.GetAll();
            var packageDetailsData = await _packageDetailsMasterRepo.GetAll();
            var hotelMasterData = await _hotelMasterRepo.GetAll();
            var vehicleMasterData = await _vehicleMasterRepo.GetAll();
            var VehicleDetailsData = await _vehicleDetailsMasterRepo.GetAll();

            var places = (from pd in packageDetailsData
                          join pl in placeData on pd.PlaceId equals pl.Id
                          where pd.PackageId == id.IdInt
                          select pl).Distinct().ToList();

            var vehiclesGroupedByPlace = (from vd in VehicleDetailsData
                                          join vm in vehicleMasterData on vd.VehicleId equals vm.Id
                                          join pl in placeData on vd.PlaceId equals pl.Id
                                          where places.Any(p => p.Id == pl.Id) // Filter vehicles for places linked to the package
                                          group new { vd, vm } by pl into g
                                          select new
                                          {
                                              PlaceId = g.Key.Id,
                                              Vehicles = g.Select(async item => new VehicleDTO
                                              {
                                                  VehicleDetailsId = item.vd.VehicleId,
                                                  CarPrice = item.vd.CarPrice,
                                                  VehicleName = item.vm.VehicleName,
                                                  NumberOfSeats = item.vm.NumberOfSeats,
                                                  VehicleImagepath = await getImage(item.vd.VehicleImagepath),
                                              }).ToList()
                                          }).ToList();

            var result = (from pm in packageMasterData
                          where pm.Id == id.IdInt
                          select new PackageDTO
                          {
                              PackageName = pm.PackageName,
                              PackagePrice = pm.PackagePrice,
                              TravelAgentId = pm.TravelAgentId,
                              Region = pm.Region,
                              Imagepath = pm.Imagepath,
                              placeList = (from pm1 in packageMasterData
                                           join pd in packageDetailsData on pm1.Id equals pd.PackageId
                                           join pl in placeData on pd.PlaceId equals pl.Id
                                           where pm1.Id == id.IdInt
                                           select new PlaceDTO
                                           {
                                               placeId = pl.Id,
                                               PlaceName = pl.PlaceName,
                                               DayNumber = pd.DayNumber,
                                               HotelList = (from hm in hotelMasterData
                                                            where hm.PlaceId == pl.Id
                                                            select new HotelDTO
                                                            {
                                                                HotelId = hm.Id,
                                                                HotelName = hm.HotelName,
                                                            }).ToList(),
                                               VechileList = vehiclesGroupedByPlace.FirstOrDefault(g => g.PlaceId == pl.Id)?.Vehicles
                                           }).ToList(),
                          }).FirstOrDefault();

            if (result != null && !string.IsNullOrEmpty(result.Imagepath))
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, result.Imagepath);

                // Check if the image file exists
                if (File.Exists(filePath))
                {
                    var imageBytes = await File.ReadAllBytesAsync(filePath);
                    result.Imagepath = Convert.ToBase64String(imageBytes);
                }
            }

            return result;
        }

        [NonAction]
        public async Task<string> getImage(string path)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var filePath = Path.Combine(uploadsFolder, path);

            // Check if the image file exists
            if (File.Exists(filePath))
            {
                var imageBytes = await File.ReadAllBytesAsync(filePath);
                string image = Convert.ToBase64String(imageBytes);
                return image;

            }
            return null;
        }
*/

        public async Task<PackageDTO?> Get_package_details(IdDTO id)
        {
            var placeData = await _placemasterRepo.GetAll();
            var packageMasterData = await _packageMasterRepo.GetAll();
            var packageDetailsData = await _packageDetailsMasterRepo.GetAll();
            var hotelMasterData = await _hotelMasterRepo.GetAll();
            var vehicleMasterData = await _vehicleMasterRepo.GetAll();
            var VehicleDetailsData = await _vehicleDetailsMasterRepo.GetAll();

            var places = (from pd in packageDetailsData
                          join pl in placeData on pd.PlaceId equals pl.Id
                          where pd.PackageId == id.Idint
                          select pl).Distinct().ToList();

            var vehiclesGroupedByPlace = (from vd in VehicleDetailsData
                                          join vm in vehicleMasterData on vd.VehicleId equals vm.Id
                                          join pl in placeData on vd.PlaceId equals pl.Id
                                          where places.Any(p => p.Id == pl.Id) // Filter vehicles for places linked to the package
                                          group new { vd, vm } by pl into g
                                          select new
                                          {
                                              PlaceId = g.Key.Id,
                                              Vehicles = g.Select(async item => new VehicleDTO
                                              {
                                                  VehicleDetailsId = item.vd.Id,
                                                  CarPrice = item.vd.CarPrice,
                                                  VehicleName = item.vm.VehicleName,
                                                  NumberOfSeats = item.vm.NumberOfSeats,
                                                  VehicleImagepath = await getImage(item.vd.VehicleImages),
                                              }).ToList()
                                          }).ToList();

            var result = (from pm in packageMasterData
                          where pm.Id == id.Idint
                          select new PackageDTO
                          {
                              PackageName = pm.PackageName,
                              PackagePrice = pm.PackagePrice,
                              TravelAgentId = pm.TravelAgentId,
                              Region = pm.Region,
                              Imagepath = pm.PackageImages,

                              placeList = (from pm1 in packageMasterData
                                           join pd in packageDetailsData on pm1.Id equals pd.PackageId
                                           join pl in placeData on pd.PlaceId equals pl.Id
                                           where pm1.Id == id.Idint
                                           select new PlaceDTO
                                           {
                                               placeId = pl.Id,
                                               PlaceName = pl.PlaceName,
                                               DayNumber = pd.DayNumber,
                                               PlaceImagepath = pd.PlaceImages,
                                               Iterinary = pd.Iterinary,
                                               HotelList = (from hm in hotelMasterData
                                                            where hm.PlaceId == pl.Id
                                                            select new HotelDTO
                                                            {
                                                                HotelId = hm.Id,
                                                                HotelName = hm.HotelName,
                                                                HotelImagepath = hm.HotelImages, // Just assign the path here
                                                            }).ToList(),
                                           }).ToList(),
                          }).FirstOrDefault();

            if (result != null && !string.IsNullOrEmpty(result.Imagepath))
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, result.Imagepath);

                // Check if the image file exists
                if (File.Exists(filePath))
                {
                    var imageBytes = await File.ReadAllBytesAsync(filePath);
                    result.Imagepath = Convert.ToBase64String(imageBytes);
                } // Calculate the image data for the package
            }

            // Populate the VechileList for each place
            if (result != null && result.placeList != null)
            {
                foreach (var place in result.placeList)
                {
                    place.PlaceImagepath = await getImage(place.PlaceImagepath);

                    foreach (var hotel in place.HotelList)
                    {
                        hotel.HotelImagepath = await getImage(hotel.HotelImagepath);
                    }
                }

                foreach (var place in result.placeList)
                {
                    var vehiclesForPlace = vehiclesGroupedByPlace.FirstOrDefault(g => g.PlaceId == place.placeId)?.Vehicles;
                    if (vehiclesForPlace != null)
                    {
                        place.VechileList = new List<VehicleDTO>();
                        foreach (var vehicleTask in vehiclesForPlace)
                        {
                            var vehicle = await vehicleTask; // Await the task to get the VehicleDTO
                            place.VechileList.Add(vehicle); // Add the VehicleDTO to VechileList
                        }
                    }
                }
            }


            return result;
        }

        [NonAction]
        public async Task<string?> getImage(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                // Handle the case where the path is null or empty
                return null;
            }
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var filePath = Path.Combine(uploadsFolder, path);

            // Check if the image file exists
            if (File.Exists(filePath))
            {
                var imageBytes = await File.ReadAllBytesAsync(filePath);
                string image = Convert.ToBase64String(imageBytes);
                return image;
            }
            return null;
        }
    }
}
