using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class ChangePasswordDto
    {
        [Required]
        [Display(Name = "Old Password")]
        public string oldPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string newPassword { get; set; }
    }
}
