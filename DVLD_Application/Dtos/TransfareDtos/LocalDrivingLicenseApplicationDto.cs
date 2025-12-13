using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class LocalDrivingLicenseApplicationDto
    {
        [Key, ForeignKey("Application")]
        public int ApplicationID { get;  set; }

        public int LicenseClassID { get;  set; }

        public virtual ApplicationDto Application { get;  set; }

        [ForeignKey("LicenseClassID")]
        public virtual LicenseClassDto LicenseClass { get;  set; }

        public virtual ICollection<AppointmentDto> Appointments { get;  set; } = new HashSet<AppointmentDto>();


      
    }
}
