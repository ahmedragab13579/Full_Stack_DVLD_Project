using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class AppointmentDto:BaseEntityDto
    {
        public int TestTypeID { get;  set; } 
        public int LocalDrivingLicenseApplicationID { get;  set; } 

        public DateTime AppointmentDate { get;  set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidFees { get;  set; }

        public bool IsLocked { get;  set; }

        [ForeignKey("TestTypeID")]
        public virtual TestTypeDto TestType { get;  set; }

        [ForeignKey("LocalDrivingLicenseApplicationID")]
        public virtual LocalDrivingLicenseApplicationDto LocalDrivingLicenseApplication { get;  set; }

        public virtual TestDto Test { get;  set; }



       
    }
}
