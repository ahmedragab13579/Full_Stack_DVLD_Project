using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class Country
    {
        [Key]
        public int Id { get; private set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }
        public virtual ICollection<Person> People { get; private set; } = new HashSet<Person>();
        private Country() { }
    }
}
