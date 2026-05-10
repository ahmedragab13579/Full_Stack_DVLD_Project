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
    public class CreateNewApplicationDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        [Display(Name = "Person ID")]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ApplicationTypeID must be a positive integer.")]
        [Display(Name = "Application Type ID")]
        public int ApplicationTypeID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Fees must be non-negative.")]
        public decimal Fees { get; set; }
    }
}
