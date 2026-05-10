using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class DriverDto
    {
        [Key, ForeignKey("Person")]
        public int Id { get;  set; }

        [Required]
        public int CreatedByUserID { get;  set; } 

        [Required]
        public DateTime CreatedDate { get;  set; }

        public virtual PersonDto Person { get;  set; }

        [ForeignKey("CreatedByUserID")]
        public virtual UserDto CreatedByUser { get;  set; }

        public virtual ICollection<LicenseDto> Licenses { get;  set; } = new HashSet<LicenseDto>();

        public virtual ICollection<InternationalLicenseDto> InternationalLicenses { get;  set; } = new HashSet<InternationalLicenseDto>();

    }
}
