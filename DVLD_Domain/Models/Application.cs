using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class Application:BaseEntity
    {
        public int PersonID { get; private set; } 
        [ForeignKey("PersonID")]
        public virtual Person Person { get; private set; }
        public DateTime ApplicationDate { get; private set; } 
        public int ApplicationTypeID { get; private set; } 
        [ForeignKey("ApplicationTypeID")]
        public virtual ApplicationType ApplicationType { get; private set; }
        public ApplicationStatus Status { get; private set; } 
        public DateTime LastStatusDate { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get; private set; }



       public virtual LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; private set; }

        public virtual InternationalLicense InternationalLicense { get; private set; }

        public virtual License License { get; private set; }

        public void UpdateStatus(ApplicationStatus newStatus,int updatedbyuserid)
        {
            if(this.Status== ApplicationStatus.Completed)
                throw new InvalidOperationException("Cannot change status of a completed application.");
            if(this.Status== ApplicationStatus.Cancelled)
                throw new InvalidOperationException("Cannot change status of a cancelled application.");
            if(newStatus == ApplicationStatus.New&&this.Status==ApplicationStatus.InProgress)
                throw new InvalidOperationException("Cannot revert status back to New from InProgress.");
            if (Status != newStatus)
            {
                Status = newStatus;
                LastStatusDate = DateTime.UtcNow;
            }

            base.UpdateModificationInfo(updatedbyuserid);
        }

        private Application()
        {
            
        }
        public Application(int Id, int applicationTypeID, decimal fees,int? createdbyuserid):base(createdbyuserid)
        {
            if(Id<=0)
                throw new ArgumentException("Id must be a positive integer.", nameof(Id));
            if(applicationTypeID<=0)
                throw new ArgumentException("ApplicationTypeID must be a positive integer.", nameof(applicationTypeID));
            if(fees<0)
                throw new ArgumentException("Fees cannot be negative.", nameof(fees));
            Id = Id;
            ApplicationTypeID = applicationTypeID;
            Fees = fees;
            ApplicationDate = DateTime.UtcNow;
            Status = ApplicationStatus.New;
            LastStatusDate = DateTime.UtcNow;
        }
    }
}
