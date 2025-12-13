using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateApplicationDto
    {
       
        [Required]
        [Display(Name = "Application Id")]
        public int applicationId { get; set; }
        [Required]
        [Display(Name = "New Status")]
        public ApplicationStatus newStatus { get; set; }
    }
}
