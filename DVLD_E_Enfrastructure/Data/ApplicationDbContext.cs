using DVLD_Domain.Models;
using DVLD_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationType> ApplicationTypes { get; set; }

        public DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }
        public DbSet<InternationalLicense> InternationalLicenses { get; set; }
        public DbSet<License> Licenses { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<DetainedLicense> DetainedLicenses { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<LicenseClass> LicenseClasses { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestType> TestTypes { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<UserLoginAudit> userUserLoginAudits { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var e in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(e.ClrType))
                {
                    var parameter = Expression.Parameter(e.ClrType, "e");
                    var body = Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false));
                    modelBuilder.Entity(e.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
                }
            }


            modelBuilder.Entity<Person>()
                .HasOne(p=>p.User)
                .WithOne(u=>u.Person)
                .HasForeignKey<User>(User=> User.PersonID);
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Driver)
                .WithOne(d => d.Person)
                .HasForeignKey<Driver>(d => d.PersonID);
            modelBuilder.Entity<Application>()
                .HasOne(a=>a.LocalDrivingLicenseApplication)
                .WithOne(ldla=>ldla.Application)
                .HasForeignKey<LocalDrivingLicenseApplication>(ldla=>ldla.ApplicationID);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Test)
                .WithOne(t => t.Appointment)
                .HasForeignKey<Test>(t => t.AppointmentID);
            modelBuilder.Entity<Person>()
                .HasIndex(p=>p.NationalNo)
                .IsUnique();



            foreach(var e in modelBuilder.Model.GetEntityTypes().SelectMany(t=>t.GetProperties()).Where(p=>p.ClrType==typeof(decimal)||p.ClrType==typeof(decimal?)))
            {
                e.SetColumnType("decimal(18,2)");

            }

            foreach (var f in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
            {
                f.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
