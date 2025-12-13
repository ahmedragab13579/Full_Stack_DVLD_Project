using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Applications;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Applicaion
{
    public class ApplicationTypeService : IApplicationTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationTypeService> _logger;
        public ApplicationTypeService(ApplicationDbContext context, IMapper mapper, ILogger<ApplicationTypeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
     
        public async Task<Result<List<ApplicationTypeDto>>> GetAllAsync()
        {
            var dtos = await _context.ApplicationTypes
                           .AsNoTracking()
                           .ProjectTo<ApplicationTypeDto>(_mapper.ConfigurationProvider)
                           .ToListAsync();
            return Result<List<ApplicationTypeDto>>.Success(dtos);
        }

        public async Task<Result<ApplicationTypeDto>> GetByIdAsync(int id)
        {
            var app = await _context.ApplicationTypes.AsNoTracking().ProjectTo<ApplicationTypeDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.Id == id);
            if (app == null)
                return Result<ApplicationTypeDto>.Failure("Not Found", "NOT_FOUND");

            return Result<ApplicationTypeDto>.Success(app);
        }
    
        public async Task<Result<bool>> UpdateAsync(UpdateApplicationTypeDto dto,int userid)
        {
            var entity = await _context.ApplicationTypes.FindAsync(dto.id);
            if (entity == null)
                return Result<bool>.Failure("Not Found", "NOT_FOUND");
            try
            {
                entity.UpdateDetails(dto.Title, dto.Fees,userid);
                bool succsses = await _context.SaveChangesAsync() > 0;
                if (!succsses)
                    return Result<bool>.Failure("Update Failed", "UPDATE_FAILED");
                return Result<bool>.Success(succsses);
            }
            catch (ArgumentException dbEx)
            {
                _logger.LogError(dbEx, "Validation error updating ApplicationType with Id {Id}", dto.id);
                return Result<bool>.Failure(dbEx.Message, "VALIDATION_ERROR");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ApplicationType with Id {Id}", dto.id);
                return Result<bool>.Failure("An error occurred while updating the ApplicationType.", "UPDATE_ERROR");
            }
        }

    }
}
