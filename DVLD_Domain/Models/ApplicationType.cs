using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Domain.Models
{
    public class ApplicationType:BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; private set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Fees { get; private set; }
        public virtual ICollection<Application> Applications { get; private set; } = new HashSet<Application>();

        private ApplicationType()
        {
            
        }

        public ApplicationType(string title, decimal fees,int? createdbyuserid):base(createdbyuserid)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            if (fees < 0)
                throw new ArgumentException("Fees cannot be negative.", nameof(fees));
            Title = title;
            Fees = fees;
        }


        public void UpdateDetails(string title, decimal fees,int userid)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            if (fees < 0)
                throw new ArgumentException("Fees cannot be negative.", nameof(fees));
            Title = title;
            Fees = fees;
            base.UpdateModificationInfo(userid);
        }   
    }
}
