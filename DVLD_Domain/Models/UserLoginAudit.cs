using DVLD_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Domain.Models
{
    public class UserLoginAudit
    {
        public long Id { get; private set; } 

        public int UserId { get; private set; } 

        [ForeignKey("UserId")]
        public virtual User User { get; private set; }

        public DateTime Timestamp { get; private set; }

        public bool Success { get; private set; } 

        [MaxLength(50)]
        public string IPAddress { get; private set; } 

        private UserLoginAudit() { }

        public UserLoginAudit(int userId, bool success, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("IP Address is required.", nameof(ipAddress));
            if(userId<=0)
                throw new ArgumentException("Invalid User ID.", nameof(userId));
            UserId = userId;
            Timestamp = DateTime.UtcNow;
            Success = success;
            IPAddress = ipAddress;
        }
    }
}
