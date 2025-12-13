using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class InternationalLicenseDto:BaseEntityDto
    {
        public int ApplicationID { get;  set; } 
        public int DriverID { get;  set; }
        public int IssuedUsingLocalLicenseID { get;  set; } 
        public DateTime IssueDate { get;  set; }
        public DateTime ExpirationDate { get;  set; }
        public bool IsActive { get;  set; }

        [ForeignKey("ApplicationID")]
        public virtual ApplicationDto Application { get;  set; }

        [ForeignKey("DriverID")]
        public virtual DriverDto Driver { get;  set; }

        [ForeignKey("IssuedUsingLocalLicenseID")]
        public virtual LicenseDto LocalLicense { get;  set; }


    }
}
