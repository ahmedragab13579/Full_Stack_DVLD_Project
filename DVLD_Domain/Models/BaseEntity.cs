using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class BaseEntity
    {
        public int Id { get;private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public int? createdbyuserid { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public int? UpdatedByUserId { get; private set; }

        [ForeignKey("createdbyuserid")]
        public virtual User CreatedByUser { get; private set; }

        [ForeignKey("UpdatedByUserId")]
        public virtual User UpdatedByUser { get; private set; }


        protected BaseEntity()
        {

        }


        public void MarkAsDeleted(int userid, bool status)
        {
            if (status == this.IsDeleted)
                throw new InvalidOperationException($"Entity is already marked as {(status ? "deleted" : "active")}.");
            IsDeleted = status;
            UpdatedAt = DateTime.UtcNow;
            UpdatedByUserId = userid;
        }
        public void UpdateModificationInfo(int userid)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedByUserId = userid;
        }
        public BaseEntity(int? createdbyuserid)
        {
            if (createdbyuserid <= 0)
                throw new ArgumentException("createdbyuserid must be a positive integer.", nameof(createdbyuserid));
            this.createdbyuserid = createdbyuserid;
            CreatedAt = DateTime.UtcNow;
        }
    }
}