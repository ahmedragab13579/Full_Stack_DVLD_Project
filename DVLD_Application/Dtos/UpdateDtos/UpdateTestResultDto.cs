using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateTestResultDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsPassed { get; set; }

    }
}
