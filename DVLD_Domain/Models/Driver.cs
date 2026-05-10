using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class Driver
    {
        [Key, ForeignKey("Person")]
        public int Id { get; private set; }

        [Required]
        public int? createdbyuserid { get; private set; } 

        [Required]
        public DateTime CreatedDate { get; private set; }

        public virtual Person Person { get; private set; }

        [ForeignKey("createdbyuserid")]
        public virtual User CreatedByUser { get; private set; }

        public virtual ICollection<License> Licenses { get; private set; } = new HashSet<License>();

        public virtual ICollection<InternationalLicense> InternationalLicenses { get; private set; } = new HashSet<InternationalLicense>();

        private Driver()
        {
        }


        public Driver(int Id, int? createdbyuserid)
        {
            if (Id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(Id));
            if (createdbyuserid <= 0)
                throw new ArgumentException("createdbyuserid must be a positive integer.", nameof(createdbyuserid));
            Id = Id;
            createdbyuserid = createdbyuserid;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
