using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Domain.Models
{
    public class User
    {
        [Key, ForeignKey("Person")]
        public int PersonID { get; private set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; private set; }

        [Required]
        public string PasswordHash { get; private set; }
    

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public int CreatedByUserId { get; private set; } 
        public DateTime? UpdatedAt { get; private set; }
        public int? UpdatedByUserId { get; private set; }

        public virtual Person Person { get; private set; }

        private User()
        {
        }

        public void Deactivate(int updatedByUserId)
        {
            if (!IsActive)
                throw new InvalidOperationException("User is already inactive.");
            IsActive = false;
            SetModificationInfo(updatedByUserId); 
        }

        public void Activate(int updatedByUserId)
        {
            if (IsActive)
                throw new InvalidOperationException("User is already active.");
            IsActive = true;
            SetModificationInfo(updatedByUserId);
        }

        public void UpdatePassword(string newPasswordHash,  int updatedByUserId)
        {
            if (newPasswordHash == null || newPasswordHash.Length == 0)
                throw new ArgumentException("New password hash is required.", nameof(newPasswordHash));
           

            PasswordHash = newPasswordHash;
            SetModificationInfo(updatedByUserId);
        }

        public void UpdateUserName(string newUserName, int updatedByUserId)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
                throw new ArgumentException("New UserName is required.", nameof(newUserName));
            if (newUserName.Length > 100)
                throw new ArgumentException("UserName cannot exceed 100 characters.", nameof(newUserName));

            UserName = newUserName;
            SetModificationInfo(updatedByUserId); 
        }

        public User(int personId, string userName, string passwordHash, int createdByUserId)
        {
            if (personId <= 0)
                throw new ArgumentException("PersonID must be a positive integer.", nameof(personId));
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName is required.", nameof(userName));
            if (userName.Length > 100)
                throw new ArgumentException("UserName cannot exceed 100 characters.", nameof(userName));
            if (passwordHash == null || passwordHash.Length == 0)
                throw new ArgumentException("PasswordHash is required.", nameof(passwordHash));
            if (createdByUserId <= 0)
                throw new ArgumentException("CreatedByUserId must be a positive integer.", nameof(createdByUserId));

            PersonID = personId;
            UserName = userName;
            PasswordHash = passwordHash;
            CreatedByUserId = createdByUserId; 
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        private void SetModificationInfo(int updatedByUserId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedByUserId = updatedByUserId;
        }
    }
}