using DVDL_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Domain.Models
{
    public class Person : BaseEntity
    {
        [Key]
        public int PersonID { get; private set; }

        [Required]
        [MaxLength(20)]
        public string NationalNo { get; private set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(50)]
        public string SecondName { get; private set; }

        [MaxLength(50)]
        public string ThirdName { get; private set; } 

        [Required]
        [MaxLength(50)]
        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public Gender Gender { get; private set; }

        [MaxLength(500)]
        public string Address { get; private set; } 

        [Required]
        [MaxLength(20)]
        [Phone]
        public string Phone { get; private set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; private set; } 

        [MaxLength(255)]
        public string ImagePath { get; private set; } 

        public int NationalityCountryID { get; private set; }

        [ForeignKey("NationalityCountryID")]
        public virtual Country NationalityCountry { get; private set; }
        public virtual Driver Driver { get; private set; }
        public virtual User User { get; private set; }
        public virtual ICollection<Application> Applications { get; private set; } = new HashSet<Application>();

        private Person()
        {
        }


        public void UpdateContactInfo(int updatedByUserId, string phone, string email, string address)
        {
            if (string.IsNullOrWhiteSpace(phone)) 
                throw new ArgumentException("Phone is required.", nameof(phone));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));
            if(updatedByUserId<=0)
                throw new ArgumentException("UpdatedByUserId must be a positive integer.", nameof(updatedByUserId));

            Phone = phone;
            Email = email; 
            Address = address;
            base.UpdateModificationInfo(updatedByUserId);
        }

        public void UpdateImagePath(int updatedByUserId, string imagePath)
        {
            if ( updatedByUserId<=0)
                throw new ArgumentException("UpdatedByUserId must be a positive integer.", nameof(updatedByUserId));
            ImagePath = imagePath;
            base.UpdateModificationInfo(updatedByUserId);
        }

        public void UpdateNames(int updatedByUserId, string firstName, string secondName, string thirdName, string lastName)
        {
            if(updatedByUserId <= 0)
                throw new ArgumentException("UpdatedByUserId must be a positive integer.", nameof(updatedByUserId));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(secondName))
                throw new ArgumentException("SecondName is required.", nameof(secondName));
            if (string.IsNullOrWhiteSpace(thirdName))
                throw new ArgumentException("ThirdName is required.", nameof(thirdName));   
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName is required.", nameof(lastName));

            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName; 
            LastName = lastName;
            base.UpdateModificationInfo(updatedByUserId);
        }

        public void UpdateDateOfBirth(int updatedByUserId, DateTime dateOfBirth)
        {
            if (dateOfBirth >= DateTime.UtcNow)
                throw new ArgumentException("DateOfBirth must be in the past.", nameof(dateOfBirth));

            DateOfBirth = dateOfBirth;
            base.UpdateModificationInfo(updatedByUserId);
        }

        public Person(
            string nationalNo, string firstName, string secondName, string lastName,
            DateTime dateOfBirth, Gender gender, string phone, int nationalityCountryID, int createdByUserId,
            string thirdName, string email, string address, string imagePath
        )
            : base(createdByUserId)
        {
            if (createdByUserId <= 0)
                throw new ArgumentException("CreatedByUserId must be a positive integer.", nameof(createdByUserId));
            if (string.IsNullOrWhiteSpace(nationalNo))
                throw new ArgumentException("NationalNo is required.", nameof(nationalNo));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(secondName))
                throw new ArgumentException("SecondName is required.", nameof(secondName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName is required.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is required.", nameof(phone));
            if (nationalityCountryID <= 0)
                throw new ArgumentException("NationalityCountryID must be a positive integer.", nameof(nationalityCountryID));

            if (dateOfBirth >= DateTime.UtcNow)
                throw new ArgumentException("DateOfBirth must be in the past.", nameof(dateOfBirth));

            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Phone = phone;
            NationalityCountryID = nationalityCountryID;

            ThirdName = thirdName;
            Email = email;
            Address = address;
            ImagePath = imagePath;
        }
    }
}