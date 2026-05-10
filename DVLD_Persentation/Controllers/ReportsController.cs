using DVLD_Application.Services.Interfaces.Reports;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: /Reports/Index
        public async Task<IActionResult> Index(DateTime? from, DateTime? to, string? type)
        {
            ViewBag.From = from;
            ViewBag.To = to;
            ViewBag.Type = string.IsNullOrWhiteSpace(type) ? "Applications" : type;

            object reportData = null;

            switch (ViewBag.Type)
            {
                case "Applications":
                case "Applications Summary":
                    var appResult = await _reportService.GetApplicationsSummaryAsync(from, to);
                    if (appResult.IsSuccess) reportData = appResult.Data;
                    break;
                case "Revenue Audit":
                    var revResult = await _reportService.GetRevenueAuditAsync(from, to);
                    if (revResult.IsSuccess) reportData = revResult.Data;
                    break;
                case "Driver Demographics":
                    var driverResult = await _reportService.GetDriverDemographicsAsync();
                    if (driverResult.IsSuccess) reportData = driverResult.Data;
                    break;
                case "Test Pass/Fail Rates":
                    var testResult = await _reportService.GetTestPassFailRatesAsync(from, to);
                    if (testResult.IsSuccess) reportData = testResult.Data;
                    break;
            }

            return View(reportData);
        }
    }
}
