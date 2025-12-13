using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Applications;
using DVLD_Application.Services.Interfaces.Mapping;
using DVLD_E_Enfrastructure.Data;
using DVLD_Domain.Enums;
using DVLD_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using System.Globalization;
using AutoMapper;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Applicaion
{
    public class LocalDrivingLicenseApplicationService : ILocalDrivingLicenseApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapping;
        private readonly ILogger<LocalDrivingLicenseApplicationService> _logger;
        private readonly DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService _currentUserService;

        public LocalDrivingLicenseApplicationService(
            ApplicationDbContext context,
            IMapper mapping,
            ILogger<LocalDrivingLicenseApplicationService> logger,
            DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService currentUserService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _currentUserService = currentUserService;
        }

        public async Task<Result<LocalDrivingLicenseApplicationDto>> GetLocalDrivingLicenseApplicationByIdAsync(int localAppId)
        {
            if (localAppId <= 0)
                return Result<LocalDrivingLicenseApplicationDto>.Failure("Invalid id.", "INVALID_INPUT");

            try
            {
                var entity = await _context.LocalDrivingLicenseApplications
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ldla => ldla.ApplicationID == localAppId);

                if (entity == null)
                    return Result<LocalDrivingLicenseApplicationDto>.Failure("Not found.", "NOT_FOUND");

                var dto = _mapping.Map<LocalDrivingLicenseApplication, LocalDrivingLicenseApplicationDto>(entity);
                return Result<LocalDrivingLicenseApplicationDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLocalDrivingLicenseApplicationByIdAsync failed for LocalAppId {LocalAppId}", localAppId);
                return Result<LocalDrivingLicenseApplicationDto>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<LocalDrivingLicenseApplicationDto>> GetByApplicationIdAsync(int applicationId)
        {
            return await GetLocalDrivingLicenseApplicationByIdAsync(applicationId);
        }

        public async Task<Result<List<LocalDrivingLicenseApplicationDto>>> GetAllApplicationsAsync()
        {
            try
            {
                var list = await _context.LocalDrivingLicenseApplications
                    .AsNoTracking()
                    .ProjectTo<LocalDrivingLicenseApplicationDto>(_mapping.ConfigurationProvider)
                    .OrderBy(ldla => ldla.ApplicationID)
                    .ToListAsync();

                if (list == null || list.Count == 0)
                    return Result<List<LocalDrivingLicenseApplicationDto>>.Failure("No records found.", "NOT_FOUND");
                return Result<List<LocalDrivingLicenseApplicationDto>>.Success(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllApplicationsAsync failed");
                return Result<List<LocalDrivingLicenseApplicationDto>>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<bool>> DidPassAllTestsAsync(int localAppId)
        {
            if (localAppId <= 0)
                return Result<bool>.Failure("Invalid id.", "INVALID_INPUT");
            var localapplication = _context.LocalDrivingLicenseApplications
                .AsNoTracking()
                .FirstOrDefault(ldla => ldla.ApplicationID == localAppId);
            if (localapplication == null)
                return Result<bool>.Failure("Local driving license application not found.", "NOT_FOUND");
            try
            {
                bool passedAllTests = localapplication.NumberofPassedTests==3;
                if (passedAllTests)
                    return Result<bool>.Success(true, "All tests passed.");
                else
                    return Result<bool>.Success(false, "Not all tests passed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DidPassAllTestsAsync failed for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<bool>> IsLicenseIssuedAsync(int localAppId)
        {
            if (localAppId <= 0)
                return Result<bool>.Failure("Invalid id.", "INVALID_INPUT");

            try
            {
                var issued = await _context.Licenses
                    .AsNoTracking()
                    .AnyAsync(l => l.ApplicationID == localAppId);

                return Result<bool>.Success(issued, issued ? "License is issued." : "License is not issued.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IsLicenseIssuedAsync failed for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<bool>> HasActiveApplicationForClassAsync(int personId, int licenseClassId)
        {
            if (personId <= 0 || licenseClassId <= 0)
                return Result<bool>.Failure("Invalid input.", "INVALID_INPUT");

            try
            {
                var activeStatuses = new[] { ApplicationStatus.New, ApplicationStatus.InProgress };

                var exists = await _context.Applications
                    .AsNoTracking()
                    .Where(a => a.PersonID == personId && activeStatuses.Contains(a.Status))
                    .Join(_context.LocalDrivingLicenseApplications.AsNoTracking(),
                          a => a.Id,
                          ldla => ldla.ApplicationID,
                          (a, ldla) => ldla)
                    .AnyAsync(ldla => ldla.LicenseClassID == licenseClassId);

                return Result<bool>.Success(exists, exists ? "Active application exists." : "No active application found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HasActiveApplicationForClassAsync failed for PersonId {PersonId} LicenseClassId {LicenseClassId}", personId, licenseClassId);
                return Result<bool>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<int>> CreateNewApplicationAsync(CreateNewLocalDrivingLicenseApplicationDto dto)
        {
            if (dto == null)
                return Result<int>.Failure("Request body is required.", "INVALID_INPUT");
            
            var currentUserId = _currentUserService.GetCurrentUserId();
            if(!currentUserId.HasValue) return Result<int>.Failure("Current User Not Found");

            if (dto.PersonID <= 0)
                return Result<int>.Failure("Invalid ids.", "INVALID_INPUT");
            if (dto.ApplicationID <= 0 || dto.LicenseClassID <= 0)
                return Result<int>.Failure("Invalid application data.", "INVALID_INPUT");

            try
            {
                var application = await _context.Applications
                    .FirstOrDefaultAsync(a => a.Id == dto.ApplicationID);

                if (application == null)
                    return Result<int>.Failure("Related application not found.", "NOT_FOUND");

                if (application.PersonID != dto.PersonID)
                    return Result<int>.Failure("Application does not belong to specified person.", "FORBIDDEN");

                var alreadyExists = await _context.LocalDrivingLicenseApplications
                    .AsNoTracking()
                    .AnyAsync(ldla => ldla.ApplicationID == dto.ApplicationID);

                if (alreadyExists)
                    return Result<int>.Failure("Local driving license application already exists for this application.", "CONFLICT");

                var entity = new LocalDrivingLicenseApplication(dto.ApplicationID, dto.LicenseClassID);

                await _context.LocalDrivingLicenseApplications.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Result<int>.Success(entity.ApplicationID, "Local driving license application created.");
            }
            catch (ArgumentException aex)
            {
                _logger.LogWarning(aex, "CreateNewApplicationAsync: validation failed for ApplicationId {ApplicationId}", dto?.ApplicationID);
                return Result<int>.Failure("Invalid operation.", "INVALID_OPERATION");
            }
            catch (DbUpdateException dbex)
            {
                _logger.LogError(dbex, "CreateNewApplicationAsync: database error for ApplicationId {ApplicationId}", dto?.ApplicationID);
                return Result<int>.Failure("Database error.", "ERROR");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateNewApplicationAsync: unexpected error for ApplicationId {ApplicationId}", dto?.ApplicationID);
                return Result<int>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<bool>> CancelApplicationAsync(int localAppId)
        {
            if (localAppId <= 0)
                return Result<bool>.Failure("Invalid input.", "INVALID_INPUT");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if(!currentUserId.HasValue) return Result<bool>.Failure("Current User Not Found");

            try
            {
                var ldla = await _context.LocalDrivingLicenseApplications
                    .Include(l => l.Application)
                    .FirstOrDefaultAsync(l => l.ApplicationID == localAppId);

                if (ldla == null)
                    return Result<bool>.Failure("Local driving license application not found.", "NOT_FOUND");

                var application = ldla.Application;
                if (application == null)
                    return Result<bool>.Failure("Related application not found.", "NOT_FOUND");

                application.UpdateStatus(ApplicationStatus.Cancelled, currentUserId.Value);
                _context.Applications.Update(application);

                var saved = await _context.SaveChangesAsync() > 0;
                if (!saved)
                {
                    _logger.LogWarning("CancelApplicationAsync: SaveChanges returned 0 for LocalAppId {LocalAppId}", localAppId);
                    return Result<bool>.Failure("Failed to cancel application.", "ERROR");
                }

                return Result<bool>.Success(true, "Application cancelled.");
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogWarning(ioe, "CancelApplicationAsync: invalid operation for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("Invalid operation.", "INVALID_OPERATION");
            }
            catch (DbUpdateException dbex)
            {
                _logger.LogError(dbex, "CancelApplicationAsync: database error for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("Database error.", "ERROR");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CancelApplicationAsync: unexpected error for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("An unexpected error occurred.", "ERROR");
            }
        }

        public async Task<Result<bool>> DeleteApplicationAsync(int localAppId)
        {
            if (localAppId <= 0)
                return Result<bool>.Failure("Invalid id.", "INVALID_INPUT");

            try
            {
                var entity = await _context.LocalDrivingLicenseApplications
                    .FirstOrDefaultAsync(ldla => ldla.ApplicationID == localAppId);

                if (entity == null)
                    return Result<bool>.Failure("Local driving license application not found.", "NOT_FOUND");

                _context.LocalDrivingLicenseApplications.Remove(entity);
                var saved = await _context.SaveChangesAsync() > 0;

                if (!saved)
                {
                    _logger.LogWarning("DeleteApplicationAsync: SaveChanges returned 0 for LocalAppId {LocalAppId}", localAppId);
                    return Result<bool>.Failure("Failed to delete record.", "ERROR");
                }

                return Result<bool>.Success(true, "Record deleted.");
            }
            catch (DbUpdateException dbex)
            {
                _logger.LogError(dbex, "DeleteApplicationAsync: database error for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("Database error.", "ERROR");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteApplicationAsync: unexpected error for LocalAppId {LocalAppId}", localAppId);
                return Result<bool>.Failure("An unexpected error occurred.", "ERROR");
            }
        }
    }
}
