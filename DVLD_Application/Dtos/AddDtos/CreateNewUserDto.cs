using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.AddDtos
{
    public class CreateNewUserDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        [Display(Name = "Person ID")]
        public int Id { get;  set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string UserName { get;  set; }

        [Required]
        [MaxLength(255)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get;  set; }
          
    }
}