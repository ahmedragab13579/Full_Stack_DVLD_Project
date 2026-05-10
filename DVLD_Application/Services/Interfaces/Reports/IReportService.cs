using DVLD_Application.Dtos.Reports;
using DVLD_Application.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Reports
{
    public interface IReportService
    {
        Task<Result<ReportSummaryDto>> GetApplicationsSummaryAsync(DateTime? from, DateTime? to);
        Task<Result<ReportSummaryDto>> GetRevenueAuditAsync(DateTime? from, DateTime? to);
        Task<Result<List<DriverDemographicsDto>>> GetDriverDemographicsAsync();
        Task<Result<List<TestPassFailDto>>> GetTestPassFailRatesAsync(DateTime? from, DateTime? to);
    }
}
