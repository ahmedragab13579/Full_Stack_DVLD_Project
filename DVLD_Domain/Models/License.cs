using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class License:BaseEntity
    {
        public int ApplicationID { get; private set; } 
        public int DriverID { get; private set; } 
        public int LicenseClassID { get; private set; } 

        public DateTime IssueDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        [MaxLength(500)]
        public string Notes { get; private set; }

        public bool IsActive { get; private set; }

        public LicenseIssueReason IssueReason { get; private set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get; private set; }

        [ForeignKey("ApplicationID")]
        public virtual Application Application { get; private set; }

        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; private set; }

        [ForeignKey("LicenseClassID")]
        public virtual LicenseClass LicenseClass { get; private set; }

        public virtual ICollection<DetainedLicense> DetainedLicenses { get; private set; } = new HashSet<DetainedLicense>();



        private License()
        {
        }



        public void UpdateExpirationDate(DateTime newExpirationDate, int updatedByUserId)
        {
            if (newExpirationDate <= IssueDate)
                throw new ArgumentException("New expiration date must be later than issue date.", nameof(newExpirationDate));
            ExpirationDate = newExpirationDate;
            UpdateModificationInfo(updatedByUserId);
        }


        public void UpdateNotes(string newNotes, int updatedByUserId)
        {
            Notes = newNotes;
            UpdateModificationInfo(updatedByUserId);
        }   


        public void Deactivate(int updatedByUserId)
        {
            if (!IsActive)
                throw new InvalidOperationException("License is already inactive.");
            IsActive = false;
            UpdateModificationInfo(updatedByUserId);
        }

        public void Activate(int updatedByUserId)
        {
            if (IsActive)
                throw new InvalidOperationException("License is already active.");
            IsActive = true;
            UpdateModificationInfo(updatedByUserId);
        }

        public License(int applicationID, int driverID, int licenseClassID, DateTime expirationDate, string notes,LicenseIssueReason issueReason, decimal fees, int createdByUserId): base(createdByUserId)
        {
            if (applicationID <= 0)
                throw new ArgumentException("ApplicationID must be a positive integer.", nameof(applicationID));
            if (driverID <= 0)
                throw new ArgumentException("DriverID must be a positive integer.", nameof(driverID));
            if (licenseClassID <= 0)
                throw new ArgumentException("LicenseClassID must be a positive integer.", nameof(licenseClassID));
            if (fees < 0)
                throw new ArgumentException("Fees cannot be negative.", nameof(fees));

            if(expirationDate <= DateTime.UtcNow)
                throw new ArgumentException("ExpirationDate must be a future date.", nameof(expirationDate));
            if(createdByUserId <= 0)
                throw new ArgumentException("CreatedByUserId must be a positive integer.", nameof(createdByUserId));
            ApplicationID = applicationID;
            IssueDate= DateTime.UtcNow;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            ExpirationDate = expirationDate;
            Notes = notes;
            IsActive = true;
            IssueReason = issueReason;
            Fees = fees;
        }
    }
}
