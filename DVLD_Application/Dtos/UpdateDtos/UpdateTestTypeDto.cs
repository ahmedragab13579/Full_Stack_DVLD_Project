using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateTestTypeDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get;  set; }

        [MaxLength(1000)]
        public string Description { get;  set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fees must be non-negative.")]
        public decimal Fees { get;  set; }
    }
}
