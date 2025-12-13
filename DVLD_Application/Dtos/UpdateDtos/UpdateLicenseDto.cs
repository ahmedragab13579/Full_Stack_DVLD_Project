using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.UpdateDtos
{
    public class UpdateLicenseDto
    {
        [MaxLength(500)]
        public string Notes { get; set; }
    }
}
