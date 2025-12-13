using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Tests;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Test
{
    public class TestTypeService : ITestTypeService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService currentUserService;

        public TestTypeService(ApplicationDbContext db, IMapper mapper, ICurrentUserService currentUserService)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<Result<List<TestTypeDto>>> GetAllTestTypesAsync()
        {
            var testTypes = await _db.TestTypes
                .AsNoTracking()
                .ProjectTo<TestTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result<List<TestTypeDto>>.Success(testTypes);
        }

        public async Task<Result<TestTypeDto>> GetTestTypeByIdAsync(int id)
        {
            var testType = await _db.TestTypes
                .AsNoTracking()
                .ProjectTo<TestTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (testType == null) return Result<TestTypeDto>.Failure("Test Type not found.");

            return Result<TestTypeDto>.Success(testType);
        }

        public async Task<Result<bool>> UpdateTestTypeAsync(UpdateTestTypeDto dto)
        {
            var testType = await _db.TestTypes.FindAsync(dto.Id);
            if (testType == null) return Result<bool>.Failure("Test Type not found.");

            var currentUserId = currentUserService.GetCurrentUserId();
            if (!currentUserId.HasValue) return Result<bool>.Failure("Current user not found.");

            try
            {
                testType.UpdateTestType(dto.Title, dto.Description, dto.Fees, currentUserId.Value);
                await _db.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
    }
}
