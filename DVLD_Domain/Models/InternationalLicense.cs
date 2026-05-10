using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class InternationalLicense:BaseEntity
    {
        public int ApplicationID { get; private set; } 
        public int DriverID { get; private set; }
        public int IssuedUsingLocalLicenseID { get; private set; } 
        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActive { get; private set; }

        [ForeignKey("ApplicationID")]
        public virtual Application Application { get; private set; }

        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; private set; }

        [ForeignKey("IssuedUsingLocalLicenseID")]
        public virtual License LocalLicense { get; private set; }


        private InternationalLicense()
        {
        }


        public void DeactivateLicense(int userid)
        {
            if (!IsActive)
                throw new InvalidOperationException("License is already inactive.");
            IsActive = false;
            base.UpdateModificationInfo(userid);
        }

        public void ActivateLicense(int userid)
        {
            if (IsActive)
                throw new InvalidOperationException("License is already active.");
            IsActive = true;
            base.UpdateModificationInfo(userid);
        }

        public InternationalLicense(int applicationID, int driverID, int issuedUsingLocalLicenseID,DateTime expirationDate,int? createduserid):base(createduserid)
        {
            
            if (applicationID <= 0)
                throw new ArgumentException("ApplicationID must be a positive integer.", nameof(applicationID));
            if (driverID <= 0)
                throw new ArgumentException("DriverID must be a positive integer.", nameof(driverID));
            if (issuedUsingLocalLicenseID <= 0)
                throw new ArgumentException("IssuedUsingLocalLicenseID must be a positive integer.", nameof(issuedUsingLocalLicenseID));
            if(expirationDate <= DateTime.UtcNow)
                throw new ArgumentException("ExpirationDate must be a future date.", nameof(expirationDate));

            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = DateTime.UtcNow;
            ExpirationDate = expirationDate;
            IsActive = true;
        }
    }
}
