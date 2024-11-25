using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToReportDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85e8305a-2383-4419-98bd-01e11d851070");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0b6f2a5-566c-4919-a1d3-37dd3dd7a401");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc1427a8-78c3-4b19-89f0-8b29dfa0a751");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ReportDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "018b7a94-4771-46d7-b800-4dec2b8a192b", null, "Police", "POLICE" },
                    { "ba403959-2a89-4fbd-8ff3-30078c5cd562", null, "User", "USER" },
                    { "d31b9451-7a4b-4381-b217-cdb0bdd48982", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "018b7a94-4771-46d7-b800-4dec2b8a192b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba403959-2a89-4fbd-8ff3-30078c5cd562");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d31b9451-7a4b-4381-b217-cdb0bdd48982");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ReportDetail");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85e8305a-2383-4419-98bd-01e11d851070", null, "Police", "POLICE" },
                    { "c0b6f2a5-566c-4919-a1d3-37dd3dd7a401", null, "Admin", "ADMIN" },
                    { "fc1427a8-78c3-4b19-89f0-8b29dfa0a751", null, "User", "USER" }
                });
        }
    }
}
