using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationMedicationPlans_MedicationPlans_MedicationPlanId",
                table: "MedicationMedicationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationMedicationPlans_Medications_MedicationId",
                table: "MedicationMedicationPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationMedicationPlans_MedicationPlans_MedicationPlanId",
                table: "MedicationMedicationPlans",
                column: "MedicationPlanId",
                principalTable: "MedicationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationMedicationPlans_Medications_MedicationId",
                table: "MedicationMedicationPlans",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationMedicationPlans_MedicationPlans_MedicationPlanId",
                table: "MedicationMedicationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationMedicationPlans_Medications_MedicationId",
                table: "MedicationMedicationPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationMedicationPlans_MedicationPlans_MedicationPlanId",
                table: "MedicationMedicationPlans",
                column: "MedicationPlanId",
                principalTable: "MedicationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationMedicationPlans_Medications_MedicationId",
                table: "MedicationMedicationPlans",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
