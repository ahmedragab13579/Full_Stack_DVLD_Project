using System;
using System.Collections.Generic;

namespace DVLD_Application.Dtos.Reports
{
    public class ReportSummaryDto
    {
        public string Title { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<ReportItemDto> Items { get; set; } = new List<ReportItemDto>();
        public decimal TotalRevenue { get; set; }
        public int TotalCount { get; set; }
    }

    public class ReportItemDto
    {
        public string Category { get; set; }
        public int Count { get; set; }
        public decimal Revenue { get; set; }
        public double GrowthRate { get; set; } // Percentage
    }

    public class DriverDemographicsDto
    {
        public string Group { get; set; }
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class TestPassFailDto
    {
        public string TestType { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public double PassRate => (PassCount + FailCount) == 0 ? 0 : (double)PassCount / (PassCount + FailCount) * 100;
    }
}
