using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVDL_Domain.Models
{
    public class Appointment:BaseEntity
    {
        public int TestTypeID { get; private set; } 
        public int LocalDrivingLicenseApplicationID { get; private set; } 

        public DateTime AppointmentDate { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PaidFees { get; private set; }

        public bool IsLocked { get; private set; }

        [ForeignKey("TestTypeID")]
        public virtual TestType TestType { get; private set; }

        [ForeignKey("LocalDrivingLicenseApplicationID")]
        public virtual LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; private set; }

        public virtual Test Test { get; private set; }



        private Appointment()
        {
            
        }
        public void LockAppointment(int userId)
        {
            if (IsLocked)
                throw new InvalidOperationException("Appointment is already locked.");
            if (this.AppointmentDate < DateTime.UtcNow)
                throw new InvalidOperationException("Cannot lock an appointment that has already occurred.");
            IsLocked = true;
            base.UpdateModificationInfo(userId);
        }


        public void UnlockAppointment(int userId)
        {
            if (!IsLocked)
                throw new InvalidOperationException("Appointment is already unlocked.");
            if(this.AppointmentDate< DateTime.UtcNow)
                throw new InvalidOperationException("Cannot unlock an appointment that has already occurred.");

            IsLocked = false;
            base.UpdateModificationInfo(userId);
        }
        public Appointment(int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, decimal paidFees, int createdByUserId) : base(createdByUserId)
        {
            if (testTypeID <= 0)
                throw new ArgumentException("TestTypeID must be a positive integer.", nameof(testTypeID));
            if (localDrivingLicenseApplicationID <= 0)
                throw new ArgumentException("LocalDrivingLicenseApplicationID must be a positive integer.", nameof(localDrivingLicenseApplicationID));
            if (paidFees < 0)
                throw new ArgumentException("PaidFees cannot be negative.", nameof(paidFees));
            if(createdByUserId<0)
                throw new ArgumentException("CreatedByUserId must be a positive integer.", nameof(createdByUserId));
            if(appointmentDate < DateTime.UtcNow)
                throw new ArgumentException("AppointmentDate cannot be in the past.", nameof(appointmentDate));
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            IsLocked = false;
        }
    }
}
