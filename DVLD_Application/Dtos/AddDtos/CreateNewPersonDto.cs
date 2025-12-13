using DVLD_Domain.Enums;
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
    public class CreateNewPersonDto
    {

        [Required]
        [MaxLength(20)]
        [Display(Name = "National Number")]
        public string NationalNo { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Third Name")]
        public string ThirdName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CountryID must be a positive integer.")]
        [Display(Name = "Country ID")]
        public int CountryID { get; set; }
    }
}