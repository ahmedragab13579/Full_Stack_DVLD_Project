using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class CountryDto
    {
        [Key]
        public int Id { get;  set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get;  set; }
        public virtual ICollection<PersonDto> People { get;  set; } = new HashSet<PersonDto>();
    }
}
