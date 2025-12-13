using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class UserDto
    {
        [Key, ForeignKey("Person")]
        public int PersonID { get;  set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get;  set; }

        [Required]
        public string PasswordHash { get;  set; }
    

        public bool IsActive { get;  set; }

        public DateTime CreatedAt { get;  set; }
        public int CreatedByUserId { get;  set; } 
        public DateTime? UpdatedAt { get;  set; }
        public int? UpdatedByUserId { get;  set; }

        public virtual PersonDto Person { get;  set; }

   
    }
}