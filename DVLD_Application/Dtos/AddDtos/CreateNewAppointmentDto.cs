using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewAppointmentDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "TestTypeID must be a positive integer.")]
        [Display(Name = "Test Type ID")]
        public int TestTypeID { get;  set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "LocalDrivingLicenseApplicationID must be a positive integer.")]
        [Display(Name = "Local Driving License Application ID")]
        public int LocalDrivingLicenseApplicationID { get;  set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get;  set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "PaidFees must be non-negative.")]
        public decimal PaidFees { get;  set; }
    }
}
