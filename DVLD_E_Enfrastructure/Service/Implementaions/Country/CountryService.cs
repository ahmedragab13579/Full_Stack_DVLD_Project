using AutoMapper;
using AutoMapper.QueryableExtensions;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Country;
using DVLD_Application.Services.Interfaces.Mapping;
using DVLD_E_Enfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Country
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CountryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<CountryDto>>> GetAllCountriesAsync()
        {
            var dtos = await _context.Countries
                .AsNoTracking()
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result<List<CountryDto>>.Success(dtos);
        }

        public async Task<Result<CountryDto>> GetCountryByIdAsync(int countryId)
        {
            var country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.CountryId == countryId);
            if (country == null)
            {
                return Result<CountryDto>.Failure("Country not found.");
            }
            var dto = _mapper.Map<CountryDto>(country);
            return Result<CountryDto>.Success(dto);
        }
    }
}
