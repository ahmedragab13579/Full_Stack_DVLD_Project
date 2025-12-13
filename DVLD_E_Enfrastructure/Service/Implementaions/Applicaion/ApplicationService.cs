using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Applications;
using DVLD_Application.Services.Interfaces.Mapping;
using DVLD_Domain.Enums;
using DVLD_Domain.Models;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Applicaion
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationService> _logger;
        private readonly DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService _currentUserService;

        public ApplicationService(
            ApplicationDbContext context, 
            IMapper mapper, 
            ILogger<ApplicationService> logger,
            DVLD_Application.Services.Interfaces.Humans.User.ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> AddAsync(CreateNewApplicationDto dto)
        {
            var IsThePersonHasActiveApp = await IsPersonHasActiveApplicationAsync(dto.PersonID, dto.ApplicationTypeID);
            if (IsThePersonHasActiveApp.Data)
                return Result<int>.Failure("Person already has an active application of this type", "ACTIVE_APPLICATION_EXISTS");

            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<int>.Failure("Current user not found.");

            try
            {
                var app = new Application(dto.PersonID, dto.ApplicationTypeID, dto.Fees, currentUserId.Value);
                await _context.Applications.AddAsync(app);
                await _context.SaveChangesAsync();
                return Result<int>.Success(app.Id, "Added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding application");
                return Result<int>.Failure(ex.Message, "ERROR");
            }
        }

        public async Task<Result<bool>> CancelApplicationAsync(int appId)
        {
            return await ChangeStatusAsync(appId, ApplicationStatus.Cancelled);
        }

        public async Task<Result<bool>> CompleteApplicationAsync(int appId)
        {
            return await ChangeStatusAsync(appId, ApplicationStatus.Completed);
        }

        private async Task<Result<bool>> ChangeStatusAsync(int appId, ApplicationStatus newStatus)
        {
            var app = await _context.Applications.FindAsync(appId);
            if (app == null)
                return Result<bool>.Failure("Not Found", "NOT_FOUND");
            
            var currentUserId = _currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                if (newStatus == ApplicationStatus.Cancelled) app.UpdateStatus(ApplicationStatus.Cancelled, currentUserId.Value);
                else if (newStatus == ApplicationStatus.Completed) app.UpdateStatus(ApplicationStatus.Completed, currentUserId.Value);

                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Status updated");
            }
            catch (InvalidOperationException ex)
            {
                return Result<bool>.Failure(ex.Message, "INVALID_OPERATION");
            }
        }

        public async Task<Result<List<ApplicationDto>>> GetAllAsync()
        {
            var dtos = await _context.Applications
                .AsNoTracking() 
                .ProjectTo<ApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result<List<ApplicationDto>>.Success(dtos);
        }

        public async Task<Result<ApplicationDto>> GetByIdAsync(int id)
        {
            var app = await _context.Applications.AsNoTracking().ProjectTo<ApplicationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.Id == id);
            if (app == null)
                return Result<ApplicationDto>.Failure("Not Found", "NOT_FOUND");

            return Result<ApplicationDto>.Success(app);
        }

        public async Task<Result<bool>> IsPersonHasActiveApplicationAsync(int personId, int appTypeId)
        {
            bool hasActive = await _context.Applications
                .AnyAsync(a => a.PersonID == personId &&
                               a.ApplicationTypeID == appTypeId &&
                               (a.Status == ApplicationStatus.New || a.Status == ApplicationStatus.InProgress));

            return Result<bool>.Success(hasActive);
        }
    }
}
