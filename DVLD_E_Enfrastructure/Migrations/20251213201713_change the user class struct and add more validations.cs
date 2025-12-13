using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD_E_Enfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changetheuserclassstructandaddmorevalidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberofPassedTests",
                table: "LocalDrivingLicenseApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberofPassedTests",
                table: "LocalDrivingLicenseApplications");
        }
    }
}
