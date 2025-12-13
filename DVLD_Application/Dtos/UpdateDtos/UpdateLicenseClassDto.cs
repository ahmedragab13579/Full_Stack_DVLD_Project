using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateLicenseClassDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ClassName { get;  set; }

        [MaxLength(1000)]
        public string ClassDescription { get;  set; }

        [Range(0, 120, ErrorMessage = "MinimumAllowedAge must be between 0 and 120.")]
        public byte MinimumAllowedAge { get;  set; }

        [Range(0, 255, ErrorMessage = "DefaultValidityLength must be between 0 and 255.")]
        public byte DefaultValidityLength { get;  set; } 

        [Range(0, double.MaxValue, ErrorMessage = "ClassFees must be non-negative.")]
        public decimal ClassFees { get;  set; }
    }
}
