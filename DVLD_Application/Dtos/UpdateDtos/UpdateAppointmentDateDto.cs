using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateAppointmentDateDto
    {
        [Required]
        public int AppointmentId { get; set; }
        [Required]

        public DateTime NewAppointmentDate { get; set; }
      
    }
}
