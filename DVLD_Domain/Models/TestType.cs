using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class TestType: BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string Title { get; private set; }

        [MaxLength(1000)]
        public string Description { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fees { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; } = new HashSet<Appointment>();


        private TestType()
        {
        }
        private void SetProperties(string title, string description, decimal fees)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));
            if (title.Length > 100)
                throw new ArgumentException("Title cannot exceed 100 characters.", nameof(title));
            if (description != null && description.Length > 1000)
                throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(description));
            if (fees < 0)
                throw new ArgumentException("Fees cannot be negative.", nameof(fees));

            Title = title;
            Description = description;
            Fees = fees;
        }

        public  void UpdateTestType(string title, string description, decimal fees, int userid)
        {
            SetProperties(title, description, fees);
            UpdateModificationInfo(userid);
        }

        public TestType(string title, string description, decimal fees, int? userid) : base(userid)
        {
            SetProperties(title, description, fees);
        }
    }
}
