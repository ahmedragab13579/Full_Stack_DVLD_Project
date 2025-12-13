using DVLD_Application.Services.Interfaces.Applications;
using DVLD_Application.Services.Interfaces.Appointments;
using DVLD_Application.Services.Interfaces.Country;
using DVLD_Application.Services.Interfaces.Humans.Driver;
using DVLD_Application.Services.Interfaces.Humans.Person;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Licenses.Detain;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using DVLD_Application.Services.Interfaces.Password;
using DVLD_Application.Services.Interfaces.Tests;
using DVLD_E_Enfrastructure.Data;
using DVLD_E_Enfrastructure.Service.Implementaions.Applicaion;
using DVLD_E_Enfrastructure.Service.Implementaions.Appointment;
using DVLD_E_Enfrastructure.Service.Implementaions.Country;
using DVLD_E_Enfrastructure.Service.Implementaions.Humans.Driver;
using DVLD_E_Enfrastructure.Service.Implementaions.Humans.Person;
using DVLD_E_Enfrastructure.Service.Implementaions.Humans.User;
using DVLD_E_Enfrastructure.Service.Implementaions.License.Detain;
using DVLD_E_Enfrastructure.Service.Implementaions.License.International;
using DVLD_E_Enfrastructure.Service.Implementaions.License.Local;
using DVLD_E_Enfrastructure.Service.Implementaions.Password;
using DVLD_E_Enfrastructure.Service.Implementaions.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DVLD_E_Enfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Core Services
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPasswordService, PasswordServices>();

            // Domain Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<ICountryService, CountryService>();
            
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ILocalDrivingLicenseApplicationService, LocalDrivingLicenseApplicationService>();
            
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<ILicenseClassService, LicenseClassService>();
            services.AddScoped<IInternationalLicenseService, InternationalLicenseService>();
            services.AddScoped<IDetainLicenseService, DetainLicenseService>();
            
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestTypeService, TestTypeService>(); 
            services.AddScoped<IAppointmentService, AppointmentService>();

            return services;
        }
    }
}
