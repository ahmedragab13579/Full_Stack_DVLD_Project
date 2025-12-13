using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class TestTypeDto: BaseEntityDto
    {

        [Required]
        [MaxLength(100)]
        public string Title { get;  set; }

        [MaxLength(1000)]
        public string Description { get;  set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get;  set; }

        public virtual ICollection<AppointmentDto> Appointments { get;  set; } = new HashSet<AppointmentDto>();


      
    }
}
