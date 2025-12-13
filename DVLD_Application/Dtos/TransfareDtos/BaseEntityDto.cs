using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Dtos.TransfareDtos
{
    public class BaseEntityDto
    {
        public int Id { get; set; }

        public bool IsDeleted { get;  set; } = false;

        public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;

        public int CreatedByUserId { get;  set; }



        public DateTime? UpdatedAt { get;  set; }

        public int? UpdatedByUserId { get;  set; }
    }
}