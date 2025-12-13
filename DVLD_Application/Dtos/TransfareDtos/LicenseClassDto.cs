using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class LicenseClassDto: BaseEntityDto
    {
        [Required]
        [MaxLength(100)]
        public string ClassName { get;  set; }

        [MaxLength(1000)]
        public string ClassDescription { get;  set; }

        public byte MinimumAllowedAge { get;  set; }
        public byte DefaultValidityLength { get;  set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ClassFees { get;  set; }

        public virtual ICollection<LicenseDto> Licenses { get;  set; } = new HashSet<LicenseDto>();
        public virtual ICollection<LocalDrivingLicenseApplicationDto> LocalDrivingLicenseApplications { get;  set; } = new HashSet<LocalDrivingLicenseApplicationDto>();



  
    }
}
