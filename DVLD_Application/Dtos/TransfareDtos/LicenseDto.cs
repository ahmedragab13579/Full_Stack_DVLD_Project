using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class LicenseDto:BaseEntityDto
    {
        public int ApplicationID { get;  set; } 
        public int DriverID { get;  set; } 
        public int LicenseClassID { get;  set; } 

        public DateTime IssueDate { get;  set; }
        public DateTime ExpirationDate { get;  set; }

        [MaxLength(500)]
        public string Notes { get;  set; }

        public bool IsActive { get;  set; }

        public LicenseIssueReason IssueReason { get;  set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get;  set; }

        [ForeignKey("ApplicationID")]
        public virtual ApplicationDto Application { get;  set; }

        [ForeignKey("DriverID")]
        public virtual DriverDto Driver { get;  set; }

        [ForeignKey("LicenseClassID")]
        public virtual LicenseClassDto LicenseClass { get;  set; }

        public virtual ICollection<DetainedLicenseDto> DetainedLicenses { get;  set; } = new HashSet<DetainedLicenseDto>();



     
    }
}
