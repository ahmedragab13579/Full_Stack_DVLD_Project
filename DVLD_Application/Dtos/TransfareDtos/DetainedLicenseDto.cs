using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class DetainedLicenseDto:BaseEntityDto
    {
        public int LicenseID { get;  set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FineFees { get;  set; }

        public bool IsReleased { get;  set; } 

        public DateTime? ReleaseDate { get;  set; } 
        public int? ReleasedByUserID { get;  set; } 
        public int? ReleaseApplicationID { get;  set; } 

        [ForeignKey("LicenseID")]
        public virtual LicenseDto License { get;  set; }

        [ForeignKey("ReleasedByUserID")]
        public virtual UserDto ReleasedByUser { get;  set; }

        [ForeignKey("ReleaseApplicationID")]
        public virtual ApplicationDto ReleaseApplication { get;  set; }



    }
}
