using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewDetainedLicenseDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LicenseID must be a positive integer.")]
        public int LicenseID { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FineFees must be non-negative.")]
        public decimal FineFees { get; set; }
    }
}
