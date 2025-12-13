using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewLocalDrivingLicenseApplicationDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ApplicationID must be a positive integer.")]
        public int ApplicationID { get;  set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PersonID must be a positive integer.")]
        public int PersonID { get;  set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LicenseClassID must be a positive integer.")]
        public int LicenseClassID { get;  set; }
    }
}
