using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Patient_Id",
                table: "Patient",
                newName: "IDX_PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IDX_PATIENT_CELLPHONE",
                table: "Patient",
                column: "CellPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_DOCTOR_CRM",
                table: "Doctor",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_DOCTOR_ID",
                table: "Doctor",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_PATIENT_CELLPHONE",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IDX_DOCTOR_CRM",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IDX_DOCTOR_ID",
                table: "Doctor");

            migrationBuilder.RenameIndex(
                name: "IDX_PATIENT_ID",
                table: "Patient",
                newName: "IX_Patient_Id");
        }
    }
}
