using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateApplicationTypeDto
    {
        [Required]
        [Display(Name = "Application Type Id")]
        public int id { get;  set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "Application Type Title")]
        public string Title { get;  set; }

        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Fees must be non-negative.")]
        public decimal Fees { get;  set; }
    }
}
