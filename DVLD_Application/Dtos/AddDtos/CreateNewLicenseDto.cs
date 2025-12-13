using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewLicenseDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ApplicationID must be a positive integer.")]
        public int ApplicationID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "DriverID must be a positive integer.")]
        public int DriverID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LicenseClassID must be a positive integer.")]
        public int LicenseClassID { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fees must be non-negative.")]
        public decimal Fees { get; set; }
    }
}
