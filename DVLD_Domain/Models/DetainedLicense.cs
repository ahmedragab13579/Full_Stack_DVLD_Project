using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class DetainedLicense:BaseEntity
    {
        public int LicenseID { get; private set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FineFees { get; private set; }

        public bool IsReleased { get; private set; } 

        public DateTime? ReleaseDate { get; private set; } 
        public int? ReleasedByUserID { get; private set; } 
        public int? ReleaseApplicationID { get; private set; } 

        [ForeignKey("LicenseID")]
        public virtual License License { get; private set; }

        [ForeignKey("ReleasedByUserID")]
        public virtual User ReleasedByUser { get; private set; }

        [ForeignKey("ReleaseApplicationID")]
        public virtual Application ReleaseApplication { get; private set; }


        private DetainedLicense()
        {
        }

        public DetainedLicense(int licenseID, decimal fineFees, int? createdbyuserid):base(createdbyuserid)
        {
            if (licenseID <= 0)
                throw new ArgumentException("LicenseID must be a positive integer.", nameof(licenseID));
            if (fineFees < 0)
                throw new ArgumentException("FineFees cannot be negative.", nameof(fineFees));
            if (createdbyuserid <= 0)
                throw new ArgumentException("createdbyuserid must be a positive integer.", nameof(createdbyuserid));
            LicenseID = licenseID;
            FineFees = fineFees;
            IsReleased = false;
        }


        public void ReleaseLicense(int releasedByUserID, int releaseApplicationID)
        {
            if (IsReleased)
                throw new InvalidOperationException("The license has already been released.");
            if (releasedByUserID <= 0)
                throw new ArgumentException("ReleasedByUserID must be a positive integer.", nameof(releasedByUserID));
            if (releaseApplicationID <= 0)
                throw new ArgumentException("ReleaseApplicationID must be a positive integer.", nameof(releaseApplicationID));
            IsReleased = true;
            ReleaseDate = DateTime.UtcNow;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
            UpdateModificationInfo(releasedByUserID);
        }


    }
}
