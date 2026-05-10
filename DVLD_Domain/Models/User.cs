using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVLD_Domain.Models
{
    public class User
    {
        [Key, ForeignKey("Person")]
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; private set; }

        [Required]
        public string PasswordHash { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public int? createdbyuserid { get; private set; }

        public DateTime? UpdatedAt { get; private set; } 
        public int? UpdatedByUserId { get; private set; } 

        public virtual Person Person { get; private set; }

        private User() { }

        public User(int Id, string userName, string passwordHash, int? createdbyuserid)
        {
            if (Id <= 0) throw new ArgumentException("Invalid Id");
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("UserName is required");
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("PasswordHash is required");

            Id = Id;
            UserName = userName;
            PasswordHash = passwordHash;
            IsActive = true;

            CreatedAt = DateTime.UtcNow;
            createdbyuserid = createdbyuserid;
        }

        public void Deactivate(int updatedByUserId)
        {
            if (!IsActive) throw new InvalidOperationException("User is already inactive.");

            IsActive = false;
            UpdateAuditInfo(updatedByUserId); 
        }

        public void Activate(int updatedByUserId)
        {
            if (IsActive) throw new InvalidOperationException("User is already active.");

            IsActive = true;
            UpdateAuditInfo(updatedByUserId);
        }

        public void UpdatePassword(string newPasswordHash, int updatedByUserId)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("New password hash is required.");

            PasswordHash = newPasswordHash;
            UpdateAuditInfo(updatedByUserId);
        }

        public void UpdateUserName(string newUserName, int updatedByUserId)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
                throw new ArgumentException("New UserName is required.");

            UserName = newUserName;
            UpdateAuditInfo(updatedByUserId);
        }
        private void UpdateAuditInfo(int userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedByUserId = userId;
        }
    }
}