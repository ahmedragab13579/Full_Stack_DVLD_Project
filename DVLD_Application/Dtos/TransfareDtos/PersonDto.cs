using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class PersonDto : BaseEntityDto
    {
        [Key]
        public int PersonID { get;  set; }

        [Required]
        [MaxLength(20)]
        public string NationalNo { get;  set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get;  set; }

        [Required]
        [MaxLength(50)]
        public string SecondName { get;  set; }

        [MaxLength(50)]
        public string ThirdName { get;  set; } 

        [Required]
        [MaxLength(50)]
        public string LastName { get;  set; }

        public DateTime DateOfBirth { get;  set; }

        public Gender Gender { get;  set; }

        [MaxLength(500)]
        public string Address { get;  set; } 

        [Required]
        [MaxLength(20)]
        [Phone]
        public string Phone { get;  set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get;  set; } 

        [MaxLength(255)]
        public string ImagePath { get;  set; } 

        public int NationalityCountryID { get;  set; }

        [ForeignKey("NationalityCountryID")]
        public virtual CountryDto NationalityCountry { get;  set; }
        public virtual DriverDto Driver { get;  set; }
        public virtual UserDto User { get;  set; }
        public virtual ICollection<ApplicationDto> Applications { get;  set; } = new HashSet<ApplicationDto>();

  
    }
}