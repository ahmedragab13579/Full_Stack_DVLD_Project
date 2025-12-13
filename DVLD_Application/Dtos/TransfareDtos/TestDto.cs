using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class TestDto:BaseEntityDto
    {
        [Key, ForeignKey("Appointment")]
        public int AppointmentID { get;  set; }

        public bool ? TestResult { get;  set; } 

        [MaxLength(500)]
        public string Notes { get;  set; }

        public virtual AppointmentDto Appointment { get;  set; }




    }
}
