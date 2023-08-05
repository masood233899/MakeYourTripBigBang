using MakeYourTrip.Interfaces;
using MakeYourTrip.Models;
using MakeYourTrip.Models.DTO;
using MakeYourTrip.Repos;
using MakeYourTrip.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IUsersService, UsersService>();
//builder.Services.AddScoped<ITokenGenerate, TokenService>();
//builder.Services.AddScoped<ICrud<User, UserDTO>, UsersRepo>();
//builder.Services.AddScoped<IPlaceMastersService, PlaceMastersService>();
//builder.Services.AddScoped<ICrud<PlaceMaster, IdDTO>, PlaceMastersRepo>();
//builder.Services.AddScoped<ICrud<RoomTypeMaster, IdDTO>, RoomTypeMastersRepo>();
//builder.Services.AddScoped<ICrud<VehicleMaster, IdDTO>, VehicleMastersRepo>();
//builder.Services.AddScoped<IRoomTypeMastersService, RoomTypeMastersService>();
//builder.Services.AddScoped<IVehicleMastersService, VehicleMastersService>();
//builder.Services.AddScoped<ICrud<HotelMaster, IdDTO>, HotelMastersRepo>();
//builder.Services.AddScoped<IHotelMastersService, HotelMastersService>();
//builder.Services.AddScoped<ICrud<PackageMaster, IdDTO>, PackageMastersRepo>();
//builder.Services.AddScoped<IPackageMastersService, PackageMastersService>();
//builder.Services.AddScoped<ICrud<PackageDetailsMaster, IdDTO>, PackageDetailsMastersRepo>();
//builder.Services.AddScoped<IPackageDetailsMastersService, PackageDetailsMastersService>();
//builder.Services.AddScoped<ICrud<VehicleDetailsMaster, IdDTO>, VehicleDetailsMasterRepo>();
//builder.Services.AddScoped<IVehicleDetailsMasterService, VehicleDetailsMasterService>();
//builder.Services.AddScoped<ICrud<Booking, IdDTO>, BookingsRepo>();
//builder.Services.AddScoped<IBookingsService, BookingsService>();
//builder.Services.AddScoped<ICrud<VehicleBooking, IdDTO>, VehicleBookingsRepo>();
//builder.Services.AddScoped<IVehicleBookingsService, VehicleBookingsService>();
//builder.Services.AddScoped<IRoomDetailsMastersService, RoomDetailsMastersService>();
//builder.Services.AddScoped<ICrud<RoomDetailsMaster, IdDTO>, RoomDetailsMastersRepo>();
//builder.Services.AddScoped<IRoomBookingsService, RoomBookingsService>();
//builder.Services.AddScoped<ICrud<RoomBooking, IdDTO>, RoomBookingsRepo>();
//builder.Services.AddScoped<IImageRepo<PackageMaster, PackageFormModel>, PackageMastersRepo>();
//builder.Services.AddScoped<IImageRepo<PackageDetailsMaster, PlaceFormModel>, PackageDetailsMastersRepo>();
//builder.Services.AddScoped<IImageRepo<VehicleDetailsMaster, VehicleFormModel>, VehicleDetailsMasterRepo>();
//builder.Services.AddScoped<IImageRepo<HotelMaster, HotelFormModule>, HotelMastersRepo>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITokenGenerate, TokenService>();
builder.Services.AddScoped<ICrud<User, UserDTO>, UsersRepo>();
builder.Services.AddScoped<IPlaceMastersService, PlaceMastersService>();
builder.Services.AddScoped<ICrud<PlaceMaster, IdDTO>, PlaceMastersRepo>();
builder.Services.AddScoped<IHotelMastersService, HotelMastersService>();
builder.Services.AddScoped<ICrud<HotelMaster, IdDTO>, HotelMastersRepo>();
builder.Services.AddScoped<IPackageMastersService, PackageMastersService>();
builder.Services.AddScoped<ICrud<PackageMaster, IdDTO>, PackageMastersRepo>();
builder.Services.AddScoped<IRoomTypeMastersService, RoomTypeMastersService>();
builder.Services.AddScoped<ICrud<RoomTypeMaster, IdDTO>, RoomTypeMastersRepo>();
builder.Services.AddScoped<IRoomDetailsMastersService, RoomDetailsMastersService>();
builder.Services.AddScoped<ICrud<RoomDetailsMaster, IdDTO>, RoomDetailsMastersRepo>();
builder.Services.AddScoped<IVehicleMastersService, VehicleMastersService>();
builder.Services.AddScoped<ICrud<VehicleMaster, IdDTO>, VehicleMastersRepo>();
builder.Services.AddScoped<IVehicleDetailsMasterService, VehicleDetailsMasterService>();
builder.Services.AddScoped<ICrud<VehicleDetailsMaster, IdDTO>, VehicleDetailsMasterRepo>();
builder.Services.AddScoped<IBookingsService, BookingsService>();
builder.Services.AddScoped<ICrud<Booking, IdDTO>, BookingsRepo>();
builder.Services.AddScoped<IVehicleBookingsService, VehicleBookingsService>();
builder.Services.AddScoped<ICrud<VehicleBooking, IdDTO>, VehicleBookingsRepo>();
builder.Services.AddScoped<IRoomBookingsService, RoomBookingsService>();
builder.Services.AddScoped<ICrud<RoomBooking, IdDTO>, RoomBookingsRepo>();
builder.Services.AddScoped<IPackageDetailsMastersService, PackageDetailsMastersService>();
builder.Services.AddScoped<ICrud<PackageDetailsMaster, IdDTO>, PackageDetailsMastersRepo>();
builder.Services.AddScoped<IImageRepo<PackageMaster, PackageFormModel>, PackageMastersRepo>();
builder.Services.AddScoped<IImageRepo<PackageDetailsMaster, PlaceFormModel>, PackageDetailsMastersRepo>();
builder.Services.AddScoped<IImageRepo<VehicleDetailsMaster, VehicleFormModel>, VehicleDetailsMasterRepo>();
builder.Services.AddScoped<IImageRepo<HotelMaster, HotelFormModule>, HotelMastersRepo>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}

                     }
                 });
});



builder.Services.AddDbContext<TourPackagesContext>(
    optionsAction: options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "SQLConnection")));
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AngularCORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AngularCORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
