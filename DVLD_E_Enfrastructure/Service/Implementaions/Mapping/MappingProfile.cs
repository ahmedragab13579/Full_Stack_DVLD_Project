using DVLD_Domain.Models;
using AutoMapper;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;

namespace DVLD_Application.Service.Implementaions.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Application, ApplicationDto>().ReverseMap();
            CreateMap<ApplicationType, ApplicationTypeDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<DetainedLicense, DetainedLicenseDto>().ReverseMap();
            CreateMap<Driver, DriverDto>().ReverseMap();
            CreateMap<InternationalLicense, InternationalLicenseDto>().ReverseMap();
            CreateMap<License, LicenseDto>().ReverseMap();
            CreateMap<LicenseClass, LicenseClassDto>().ReverseMap();
            CreateMap<LocalDrivingLicenseApplication, LocalDrivingLicenseApplicationDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<TestType, TestTypeDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
