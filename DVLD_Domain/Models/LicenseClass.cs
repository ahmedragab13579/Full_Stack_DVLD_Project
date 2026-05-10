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
    public class LicenseClass: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ClassName { get; private set; }

        [MaxLength(1000)]
        public string ClassDescription { get; private set; }

        public byte MinimumAllowedAge { get; private set; }
        public byte DefaultValidityLength { get; private set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ClassFees { get; private set; }

        public virtual ICollection<License> Licenses { get; private set; } = new HashSet<License>();
        public virtual ICollection<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; private set; } = new HashSet<LocalDrivingLicenseApplication>();


        public void Updateinformation(string newClassName, string newDescription, byte newMinimumAge, byte newValidityLength, decimal newFees, int updatedbyuserid)
        {
            UpdateClassName(newClassName, updatedbyuserid);
            UpdateClassInformation( newDescription, updatedbyuserid);
            UpdateMinimumAllowedAge( newMinimumAge, updatedbyuserid);
            UpdateDefaultValidityLength( newValidityLength, updatedbyuserid);
            UpdateClassFees( newFees, updatedbyuserid);
            base.UpdateModificationInfo(updatedbyuserid);
        }
        private void UpdateClassInformation(string newDescription,int updatedbyuserid)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                throw new ArgumentException("Class description cannot be null or empty.", nameof(newDescription));
            ClassDescription = newDescription; 

        }

        private void UpdateClassFees(decimal newFees ,int updatedbyuserid)
        {
            if (newFees <= 0)
                throw new ArgumentException("Class fees cannot be negative of zero.", nameof(newFees));
            ClassFees = newFees;


        }
        private void UpdateMinimumAllowedAge(byte newMinimumAge, int updatedbyuserid)
        {
            if (newMinimumAge <= 0)
                throw new ArgumentException("Minimum allowed age must be greater than zero.", nameof(newMinimumAge));
            MinimumAllowedAge = newMinimumAge;



        }
        private void UpdateDefaultValidityLength(byte newValidityLength, int updatedbyuserid)
        {
            if (newValidityLength <= 0)
                throw new ArgumentException("Default validity length must be greater than zero.", nameof(newValidityLength));
            DefaultValidityLength = newValidityLength;


        }


        private void UpdateClassName(string newClassName, int updatedbyuserid)
        {
            if (string.IsNullOrWhiteSpace(newClassName))
                throw new ArgumentException("Class name cannot be null or empty.", nameof(newClassName));
            ClassName = newClassName;


        }



        private LicenseClass()
        {
            
        }


        public LicenseClass(string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, decimal classFees,int? createdbyuserid):base(createdbyuserid) 
        {
            if(createdbyuserid <= 0)
                throw new ArgumentException("Created by user ID must be greater than zero.", nameof(createdbyuserid));
            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentException("Class name cannot be null or empty.", nameof(className));
            if(string.IsNullOrWhiteSpace(classDescription))
                throw new ArgumentException("Class description cannot be null or empty.", nameof(classDescription));
            if (minimumAllowedAge <= 0)
                throw new ArgumentException("Minimum allowed age must be greater than zero.", nameof(minimumAllowedAge));
            if (defaultValidityLength <= 0)
                throw new ArgumentException("Default validity length must be greater than zero.", nameof(defaultValidityLength));
            if (classFees <= 0)
                throw new ArgumentException("Class fees cannot be negative of zero.", nameof(classFees));
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }
    }
}
