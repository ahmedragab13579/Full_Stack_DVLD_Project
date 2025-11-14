using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Domain.Models
{
    public class LocalDrivingLicenseApplication
    {
        [Key, ForeignKey("Application")]
        public int ApplicationID { get; private set; }

        public int LicenseClassID { get; private set; }

        public virtual Application Application { get; private set; }

        [ForeignKey("LicenseClassID")]
        public virtual LicenseClass LicenseClass { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; } = new HashSet<Appointment>();


        private LocalDrivingLicenseApplication()
        {

        }


        public LocalDrivingLicenseApplication(int applicationId, int licenseClassId) 
        {
            if (applicationId <= 0)
                throw new ArgumentException("ApplicationID must be a positive integer.", nameof(applicationId));
            if (licenseClassId <= 0)
                throw new ArgumentException("LicenseClassID must be a positive integer.", nameof(licenseClassId));
            ApplicationID = applicationId;
            LicenseClassID = licenseClassId;
        }
    }
}
