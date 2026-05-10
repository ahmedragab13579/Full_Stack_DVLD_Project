using DVLD_Application.Dtos.Reports;
using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Reports;
using DVLD_E_Enfrastructure.Data;
using DVLD_Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Reports
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ReportSummaryDto>> GetApplicationsSummaryAsync(DateTime? from, DateTime? to)
        {
            var query = _context.Applications.AsNoTracking();

            if (from.HasValue)
                query = query.Where(a => a.ApplicationDate >= from.Value);
            if (to.HasValue)
                query = query.Where(a => a.ApplicationDate <= to.Value);

            var totalStats = await query
                .Select(a => new { a.Fees })
                .ToListAsync();

            var totalCount = totalStats.Count;
            var totalRevenue = totalStats.Sum(a => a.Fees);

            var items = await query
                .GroupBy(a => a.ApplicationType.Title)
                .Select(g => new ReportItemDto
                {
                    Category = g.Key,
                    Count = g.Count(),
                    Revenue = g.Sum(a => a.Fees),
                    GrowthRate = 0
                })
                .ToListAsync();

            var summary = new ReportSummaryDto
            {
                Title = "Applications Summary",
                GeneratedAt = DateTime.UtcNow,
                TotalCount = totalCount,
                TotalRevenue = totalRevenue,
                Items = items
            };

            return Result<ReportSummaryDto>.Success(summary);
        }

        public async Task<Result<ReportSummaryDto>> GetRevenueAuditAsync(DateTime? from, DateTime? to)
        {
            var query = _context.Applications.AsNoTracking();

            if (from.HasValue)
                query = query.Where(a => a.ApplicationDate >= from.Value);
            if (to.HasValue)
                query = query.Where(a => a.ApplicationDate <= to.Value);

            var totalStats = await query
                .Select(a => new { a.Fees })
                .ToListAsync();

            var totalCount = totalStats.Count;
            var totalRevenue = totalStats.Sum(a => a.Fees);

            var items = await query
                .GroupBy(a => a.ApplicationDate.Date)
                .OrderBy(g => g.Key)
                .Select(g => new ReportItemDto
                {
                    Category = g.Key.ToString(), // Conversion handled in EF for common types
                    Count = g.Count(),
                    Revenue = g.Sum(a => a.Fees)
                })
                .ToListAsync();

            // Handle date formatting in-memory for consistency
            foreach (var item in items)
            {
                if (DateTime.TryParse(item.Category, out var date))
                {
                    item.Category = date.ToShortDateString();
                }
            }

            var summary = new ReportSummaryDto
            {
                Title = "Revenue Audit",
                GeneratedAt = DateTime.UtcNow,
                TotalCount = totalCount,
                TotalRevenue = totalRevenue,
                Items = items
            };

            return Result<ReportSummaryDto>.Success(summary);
        }

        public async Task<Result<List<DriverDemographicsDto>>> GetDriverDemographicsAsync()
        {
            var total = await _context.Drivers.CountAsync();
            if (total == 0) return Result<List<DriverDemographicsDto>>.Success(new List<DriverDemographicsDto>());

            var maleCount = await _context.Drivers
                .Where(d => d.Person.Gender == Gender.Male)
                .CountAsync();

            var femaleCount = await _context.Drivers
                .Where(d => d.Person.Gender == Gender.Female)
                .CountAsync();

            var demographics = new List<DriverDemographicsDto>
            {
                new DriverDemographicsDto { 
                    Group = "Male", 
                    Count = maleCount,
                    Percentage = (double)maleCount / total * 100
                },
                new DriverDemographicsDto { 
                    Group = "Female", 
                    Count = femaleCount,
                    Percentage = (double)femaleCount / total * 100
                }
            };

            return Result<List<DriverDemographicsDto>>.Success(demographics);
        }

        public async Task<Result<List<TestPassFailDto>>> GetTestPassFailRatesAsync(DateTime? from, DateTime? to)
        {
            var query = _context.Tests.AsNoTracking();

            // Note: Tests are linked via Appointments which have Dates
            if (from.HasValue)
                query = query.Where(t => t.Appointment.AppointmentDate >= from.Value);
            if (to.HasValue)
                query = query.Where(t => t.Appointment.AppointmentDate <= to.Value);

            var rates = await query
                .GroupBy(t => t.Appointment.TestType.Title)
                .Select(g => new TestPassFailDto
                {
                    TestType = g.Key,
                    PassCount = g.Count(t => t.TestResult == true),
                    FailCount = g.Count(t => t.TestResult == false)
                })
                .ToListAsync();

            return Result<List<TestPassFailDto>>.Success(rates);
        }
    }
}
