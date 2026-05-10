using DVLD_Domain.Models;
using DVLD_Domain.Enums;
using DVLD_E_Enfrastructure.Data;
using DVLD_E_Enfrastructure.Service.Implementaions.Password;
using Microsoft.EntityFrameworkCore;

namespace DVLD_Seeder
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordServices _passwordService;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
            _passwordService = new PasswordServices(); // الخدمة اللي أنت عملتها
        }

        public async Task SeedAsync()
        {
            // 1. إضافة الدول أولاً (لأن Person بيعتمد عليها)
            // ملحوظة: لو الـ Constructor بتاع Country برايفيت، استخدم طريقة لإضافته أو اجعله Public مؤقتاً
            // 1. إضافة الدول أولاً (لأن Person بيعتمد عليها)
            if (!await _context.Countries.AnyAsync())
            {
                // بما إن الكونستركتور بتاع كلاس Country (private)، هنستخدم SQL مباشر لإضافة الدولة
                // ده هيخلي مصر تاخد تلقائياً Id رقم 1
                await _context.Database.ExecuteSqlRawAsync("INSERT INTO Countries (Name) VALUES ('Egypt')");
                await _context.Database.ExecuteSqlRawAsync("INSERT INTO Countries (Name) VALUES ('Saudi Arabia')");
                await _context.Database.ExecuteSqlRawAsync("INSERT INTO Countries (Name) VALUES ('Jordan')");
            }

            // 2. إضافة الشخص والمستخدم (Admin) - المرحلة الأهم
            if (!await _context.Users.AnyAsync())
            {
                var hashedPassword = _passwordService.HashPassword("1234");

                // إنشاء الشخص بافتراض أن الـ CountryID هو 1
                var adminPerson = new Person(
                    "N100", "System", "Admin", "User",
                    new DateTime(2000, 1, 1), Gender.Male, "01012345678", 1,
                    "Middle", "admin@dvld.com", "Egypt", "img.png", 1); // 1 الأخيرة هي CreatedByUserId المفترضة

                // 🟢 فتح Transaction عشان نربط أوامر الـ SQL بأوامر الـ EF Core
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. إيقاف الـ Constraint (لاحظ استخدمت نفس اسم الـ Constraint اللي طلعلك في الإيرور بالظبط)
                    await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Persons NOCHECK CONSTRAINT FK_Persons_Users_createdbyuserid");

                    // 2. حفظ الشخص
                    _context.Persons.Add(adminPerson);
                    await _context.SaveChangesAsync();

                    // 3. إنشاء المستخدم وربطه بالشخص
                    var adminUser = new User(adminPerson.Id, "Admin", hashedPassword, adminPerson.Id);
                    _context.Users.Add(adminUser);
                    await _context.SaveChangesAsync();

                    // 4. إعادة تشغيل الـ Constraint
                    await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Persons CHECK CONSTRAINT FK_Persons_Users_createdbyuserid");

                    // تأكيد المعاملة بالكامل
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // لو حصل أي خطأ، تراجع عن كل حاجة عشان الداتابيز متتخربش
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error during initial seeding: {ex.Message}");
                    throw;
                }
            }

            // جلب الـ Admin ID لاستخدامه في باقي البيانات
            var systemAdmin = await _context.Users.FirstAsync();

            // 3. إضافة أنواع الطلبات (Application Types)
            if (!await _context.ApplicationTypes.AnyAsync())
            {
                _context.ApplicationTypes.AddRange(new List<ApplicationType>
                {
                    new ApplicationType("New Local Driving License", 15.50m, systemAdmin.Id),
                    new ApplicationType("Renew Driving License", 5.00m, systemAdmin.Id),
                    new ApplicationType("Replacement for Lost", 10.00m, systemAdmin.Id)
                });
            }

            // 4. إضافة فئات الرخص (License Classes)
            if (!await _context.LicenseClasses.AnyAsync())
            {
                _context.LicenseClasses.AddRange(new List<LicenseClass>
                {
                    new LicenseClass("Small Motorcycle", "Class 1", 16, 5, 15.00m, systemAdmin.Id),
                    new LicenseClass("Ordinary driving license", "Class 3", 18, 10, 20.00m, systemAdmin.Id),
                    new LicenseClass("Truck license", "Class 4", 21, 10, 50.00m, systemAdmin.Id)
                });
            }

            // 5. إضافة أنواع الاختبارات (Test Types)
            if (!await _context.TestTypes.AnyAsync())
            {
                _context.TestTypes.AddRange(new List<TestType>
                {
                    new TestType("Vision Test", "Eye examination", 10.00m, systemAdmin.Id),
                    new TestType("Written (Theory) Test", "Rules exam", 20.00m, systemAdmin.Id),
                    new TestType("Practical Test", "Street exam", 30.00m, systemAdmin.Id)
                });
            }

            // حفظ كل البيانات النهائية
            await _context.SaveChangesAsync();
        }
    }
}