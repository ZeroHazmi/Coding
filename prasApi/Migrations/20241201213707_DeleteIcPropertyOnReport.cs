using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIcPropertyOnReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27220d41-5fb5-474d-aff5-e842471b7fac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "831747dc-c095-469f-a89e-d6bb25bf1565");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6df551b-8aac-4844-b613-68a9ec04d769");

            migrationBuilder.DropColumn(
                name: "IcNumber",
                table: "Reports");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "477f7f7f-9da9-48ae-84e0-ef1bb085d308", null, "User", "USER" },
                    { "946648d2-ab1b-4451-9d1f-87bad65078e4", null, "Admin", "ADMIN" },
                    { "a99da1e4-03f4-472c-a75f-4cd66cbbecbd", null, "Police", "POLICE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "477f7f7f-9da9-48ae-84e0-ef1bb085d308");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "946648d2-ab1b-4451-9d1f-87bad65078e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a99da1e4-03f4-472c-a75f-4cd66cbbecbd");

            migrationBuilder.AddColumn<string>(
                name: "IcNumber",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27220d41-5fb5-474d-aff5-e842471b7fac", null, "Police", "POLICE" },
                    { "831747dc-c095-469f-a89e-d6bb25bf1565", null, "Admin", "ADMIN" },
                    { "f6df551b-8aac-4844-b613-68a9ec04d769", null, "User", "USER" }
                });
        }
    }
}
