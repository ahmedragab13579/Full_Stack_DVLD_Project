using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Domain.Models
{
    public class Driver
    {
        [Key, ForeignKey("Person")]
        public int PersonID { get; private set; }

        [Required]
        public int CreatedByUserID { get; private set; } 

        [Required]
        public DateTime CreatedDate { get; private set; }

        public virtual Person Person { get; private set; }

        [ForeignKey("CreatedByUserID")]
        public virtual User CreatedByUser { get; private set; }

        public virtual ICollection<License> Licenses { get; private set; } = new HashSet<License>();

        public virtual ICollection<InternationalLicense> InternationalLicenses { get; private set; } = new HashSet<InternationalLicense>();

        private Driver()
        {
        }


        public Driver(int personID, int createdByUserID)
        {
            if (personID <= 0)
                throw new ArgumentException("PersonID must be a positive integer.", nameof(personID));
            if (createdByUserID <= 0)
                throw new ArgumentException("CreatedByUserID must be a positive integer.", nameof(createdByUserID));
            PersonID = personID;
            CreatedByUserID = createdByUserID;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
