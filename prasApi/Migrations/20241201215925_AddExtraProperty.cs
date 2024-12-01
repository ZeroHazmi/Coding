using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ExtraInformation",
                table: "ReportDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a810f47-a561-43cf-aaf2-449e762b6cea", null, "User", "USER" },
                    { "ab5de88c-ff0e-48d8-97ca-c9a5171580ac", null, "Police", "POLICE" },
                    { "fe201249-d3f7-47cf-b808-561f44481526", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a810f47-a561-43cf-aaf2-449e762b6cea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab5de88c-ff0e-48d8-97ca-c9a5171580ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe201249-d3f7-47cf-b808-561f44481526");

            migrationBuilder.DropColumn(
                name: "ExtraInformation",
                table: "ReportDetail");

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
    }
}
