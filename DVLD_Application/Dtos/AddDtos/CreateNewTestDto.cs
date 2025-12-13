using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewTestDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AppointmentID must be a positive integer.")]
        public int AppointmentID { get;  set; }
    }
}
