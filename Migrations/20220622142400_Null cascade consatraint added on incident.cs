using Microsoft.EntityFrameworkCore.Migrations;

namespace testWork.Migrations
{
    public partial class Nullcascadeconsatraintaddedonincident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_IncidentId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_IncidentId",
                table: "Accounts",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "IncidentId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Incidents_IncidentId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Incidents_IncidentId",
                table: "Accounts",
                column: "IncidentId",
                principalTable: "Incidents",
                principalColumn: "IncidentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
