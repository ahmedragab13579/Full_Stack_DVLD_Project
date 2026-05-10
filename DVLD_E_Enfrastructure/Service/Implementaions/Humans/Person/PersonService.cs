using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.Person;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_E_Enfrastructure.Data;
using DVLD_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Humans.Person
{
    public class PersonService : IPersonService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> CreatePersonAsync(CreateNewPersonDto dto)
        {
            if (await _context.Persons.AnyAsync(p => p.NationalNo == dto.NationalNo))
            {
                return Result<int>.Failure("Person with this National No already exists.");
            }

            //var currentUserId = _currentUserService.GetCurrentUserId();
            //if(!currentUserId.HasValue) return Result<int>.Failure("Current User Not Found");

            try
            {
                var person = new DVLD_Domain.Models.Person(
                    dto.NationalNo,
                    dto.FirstName,
                    dto.SecondName,
                    dto.LastName,
                    dto.DateOfBirth,
                    dto.Gender,
                    dto.Phone,
                    dto.CountryID,
                    dto.ThirdName,
                    dto.Email,
                    dto.Address,
                    dto.ImagePath
                );

                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();

                return Result<int>.Success(person.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Failed to create person: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeletePersonAsync(int Id)
        {
            var person = await _context.Persons.FindAsync(Id);
            if (person == null)
            {
                return Result<bool>.Failure("Person not found.");
            }

            try
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FK_")) 
                {
                     return Result<bool>.Failure("Cannot delete person because they are linked to other records.");
                }
                return Result<bool>.Failure($"Failed to delete person: {ex.Message}");
            }
        }

        public async Task<Result<List<PersonDto>>> GetAllPeopleAsync()
        {
            var dtos = await _context.Persons
                .Include(p => p.User)
                .AsNoTracking()
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<PersonDto>>.Success(dtos);
        }

        public async Task<Result<PersonDto>> GetPersonByIdAsync(int Id)
        {
            var person = await _context.Persons
                .Include(p => p.NationalityCountry)
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Id);

            if (person == null)
            {
                return Result<PersonDto>.Failure("Person not found.");
            }

            var dto = _mapper.Map<PersonDto>(person);
            return Result<PersonDto>.Success(dto);
        }

        public async Task<Result<PersonDto>> GetPersonByNationalNoAsync(string nationalNo)
        {
            var person = await _context.Persons
                .Include(p => p.NationalityCountry)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.NationalNo == nationalNo);

            if (person == null)
            {
                return Result<PersonDto>.Failure("Person not found.");
            }

            var dto = _mapper.Map<PersonDto>(person);
            return Result<PersonDto>.Success(dto);
        }

        public async Task<Result<bool>> IsPersonExistAsync(string nationalNo)
        {
            var exists = await _context.Persons.AnyAsync(p => p.NationalNo == nationalNo);
            return Result<bool>.Success(exists);
        }

        public async Task<Result<bool>> IsPersonExistAsync(int Id)
        {
             var exists = await _context.Persons.AnyAsync(p => p.Id == Id);
             return Result<bool>.Success(exists);
        }

        public async Task<Result<bool>> UpdatePersonAsync(UpdatePersonDto dto)
        {
             var person = await _context.Persons.FirstOrDefaultAsync(p => p.NationalNo == dto.NationalNo);
             if (person == null)
             {
                 return Result<bool>.Failure("Person not found.");
             }

              var currentUserId = _currentUserService.GetCurrentUserId();
              if(!currentUserId.HasValue) return Result<bool>.Failure("Current User Not Found");

              try
              {
                  person.UpdateAllInformation(
                      dto.Phone,
                      dto.Email,
                      dto.Address,
                      dto.FirstName,
                      dto.SecondName,
                      dto.ThirdName,
                      dto.LastName,
                      dto.DateOfBirth,
                      dto.ImagePath,
                      currentUserId.Value
                  );

                 await _context.SaveChangesAsync();
                 return Result<bool>.Success(true);
             }
             catch (Exception ex)
             {
                 return Result<bool>.Failure($"Failed to update person: {ex.Message}");
             }
        }
    }
}
