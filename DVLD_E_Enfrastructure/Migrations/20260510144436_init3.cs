using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD_E_Enfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_CreatedByUserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTypes_Users_CreatedByUserId",
                table: "ApplicationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_CreatedByUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DetainedLicenses_Users_CreatedByUserId",
                table: "DetainedLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_CreatedByUserID",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_InternationalLicenses_Users_CreatedByUserId",
                table: "InternationalLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseClasses_Users_CreatedByUserId",
                table: "LicenseClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Users_CreatedByUserId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_CreatedByUserId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Users_CreatedByUserId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypes_Users_CreatedByUserId",
                table: "TestTypes");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Users",
                newName: "createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "TestTypes",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_TestTypes_CreatedByUserId",
                table: "TestTypes",
                newName: "IX_TestTypes_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Tests",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CreatedByUserId",
                table: "Tests",
                newName: "IX_Tests_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Persons",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_CreatedByUserId",
                table: "Persons",
                newName: "IX_Persons_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Licenses",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Licenses_CreatedByUserId",
                table: "Licenses",
                newName: "IX_Licenses_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "LicenseClasses",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_LicenseClasses_CreatedByUserId",
                table: "LicenseClasses",
                newName: "IX_LicenseClasses_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "InternationalLicenses",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_InternationalLicenses_CreatedByUserId",
                table: "InternationalLicenses",
                newName: "IX_InternationalLicenses_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserID",
                table: "Drivers",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_CreatedByUserID",
                table: "Drivers",
                newName: "IX_Drivers_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "DetainedLicenses",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_DetainedLicenses_CreatedByUserId",
                table: "DetainedLicenses",
                newName: "IX_DetainedLicenses_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Appointments",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CreatedByUserId",
                table: "Appointments",
                newName: "IX_Appointments_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "ApplicationTypes",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationTypes_CreatedByUserId",
                table: "ApplicationTypes",
                newName: "IX_ApplicationTypes_createdbyuserid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Applications",
                newName: "createdbyuserid");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_CreatedByUserId",
                table: "Applications",
                newName: "IX_Applications_createdbyuserid");

            migrationBuilder.AlterColumn<int>(
                name: "createdbyuserid",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_createdbyuserid",
                table: "Applications",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTypes_Users_createdbyuserid",
                table: "ApplicationTypes",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_createdbyuserid",
                table: "Appointments",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetainedLicenses_Users_createdbyuserid",
                table: "DetainedLicenses",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_createdbyuserid",
                table: "Drivers",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InternationalLicenses_Users_createdbyuserid",
                table: "InternationalLicenses",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseClasses_Users_createdbyuserid",
                table: "LicenseClasses",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Users_createdbyuserid",
                table: "Licenses",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Users_createdbyuserid",
                table: "Persons",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Users_createdbyuserid",
                table: "Tests",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypes_Users_createdbyuserid",
                table: "TestTypes",
                column: "createdbyuserid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Users_createdbyuserid",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTypes_Users_createdbyuserid",
                table: "ApplicationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_createdbyuserid",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DetainedLicenses_Users_createdbyuserid",
                table: "DetainedLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_createdbyuserid",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_InternationalLicenses_Users_createdbyuserid",
                table: "InternationalLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_LicenseClasses_Users_createdbyuserid",
                table: "LicenseClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_Users_createdbyuserid",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_createdbyuserid",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Users_createdbyuserid",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTypes_Users_createdbyuserid",
                table: "TestTypes");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Users",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "TestTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestTypes_createdbyuserid",
                table: "TestTypes",
                newName: "IX_TestTypes_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Tests",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_createdbyuserid",
                table: "Tests",
                newName: "IX_Tests_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Persons",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_createdbyuserid",
                table: "Persons",
                newName: "IX_Persons_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Licenses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Licenses_createdbyuserid",
                table: "Licenses",
                newName: "IX_Licenses_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "LicenseClasses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_LicenseClasses_createdbyuserid",
                table: "LicenseClasses",
                newName: "IX_LicenseClasses_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "InternationalLicenses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_InternationalLicenses_createdbyuserid",
                table: "InternationalLicenses",
                newName: "IX_InternationalLicenses_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Drivers",
                newName: "CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_createdbyuserid",
                table: "Drivers",
                newName: "IX_Drivers_CreatedByUserID");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "DetainedLicenses",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DetainedLicenses_createdbyuserid",
                table: "DetainedLicenses",
                newName: "IX_DetainedLicenses_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Appointments",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_createdbyuserid",
                table: "Appointments",
                newName: "IX_Appointments_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "ApplicationTypes",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationTypes_createdbyuserid",
                table: "ApplicationTypes",
                newName: "IX_ApplicationTypes_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "createdbyuserid",
                table: "Applications",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_createdbyuserid",
                table: "Applications",
                newName: "IX_Applications_CreatedByUserId");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_CreatedByUserId",
                table: "Applications",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTypes_Users_CreatedByUserId",
                table: "ApplicationTypes",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_CreatedByUserId",
                table: "Appointments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetainedLicenses_Users_CreatedByUserId",
                table: "DetainedLicenses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_CreatedByUserID",
                table: "Drivers",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InternationalLicenses_Users_CreatedByUserId",
                table: "InternationalLicenses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseClasses_Users_CreatedByUserId",
                table: "LicenseClasses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_Users_CreatedByUserId",
                table: "Licenses",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Users_CreatedByUserId",
                table: "Persons",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Users_CreatedByUserId",
                table: "Tests",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTypes_Users_CreatedByUserId",
                table: "TestTypes",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
