using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class Test:BaseEntity
    {
        [Key, ForeignKey("Appointment")]
        public int AppointmentID { get; private set; }

        public bool ? TestResult { get; private set; } 

        [MaxLength(500)]
        public string Notes { get; private set; }

        public virtual Appointment Appointment { get; private set; }




        public void UpdateTestResult(bool testResult, string notes, int updatedByUserId)
        {
            if (updatedByUserId <= 0)
                throw new ArgumentException("UpdatedByUserId must be a positive integer.", nameof(updatedByUserId));
            if (notes != null && notes.Length > 500)
                throw new ArgumentException("Notes cannot exceed 500 characters.", nameof(notes));
            if(this.TestResult!=null)
                throw new InvalidOperationException("Test result has already been set and cannot be modified.");
            TestResult = testResult;
            Notes = notes;
            UpdateModificationInfo(updatedByUserId);
        }


        private Test()
        {
        }

        public Test(int appointmentId,int createdByUserId): base(createdByUserId)
        {
            if (appointmentId <= 0)
                throw new ArgumentException("AppointmentID must be a positive integer.", nameof(appointmentId));
            if(createdByUserId <= 0)
                throw new ArgumentException("CreatedByUserId must be a positive integer.", nameof(createdByUserId));
            AppointmentID = appointmentId;
            TestResult = null;
        }
    }
}
