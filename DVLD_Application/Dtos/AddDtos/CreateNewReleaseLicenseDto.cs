using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewReleaseLicenseDto
    {
        [Required]
        [Display(Name = "DetainedLicenseID")]
        public int DetainedLicenseID { get; set; }
    }
}
