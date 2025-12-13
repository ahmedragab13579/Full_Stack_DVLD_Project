using DVLD_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class ApplicationDto:BaseEntityDto
    {
        public int PersonID { get;  set; } 
        [ForeignKey("PersonID")]
        public DateTime ApplicationDate { get;  set; } 
        public int ApplicationTypeID { get;  set; } 
        [ForeignKey("ApplicationTypeID")]
        public ApplicationStatus Status { get;  set; } 
        public DateTime LastStatusDate { get;  set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get;  set; }

   
    }
}
