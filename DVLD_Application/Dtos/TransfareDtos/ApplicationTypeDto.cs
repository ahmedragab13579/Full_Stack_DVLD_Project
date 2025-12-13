using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class ApplicationTypeDto:BaseEntityDto
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(150)]
        public string Title { get;  set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Fees { get;  set; }

     
    }
}
