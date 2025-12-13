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
    public class CreateNewDriverDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PersonID must be a positive integer.")]
        public int PersonID { get; set; }
    }
}
