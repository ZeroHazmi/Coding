using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class RetrySeedReportType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ddee1dc-b9be-48fe-8d58-c1cc59c9ed29");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4d2f454-5002-447c-bfd4-510b96fdeae9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e11f4f6b-cd98-44ff-a176-a741b8709f8c");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ddee1dc-b9be-48fe-8d58-c1cc59c9ed29", null, "User", "USER" },
                    { "c4d2f454-5002-447c-bfd4-510b96fdeae9", null, "Police", "POLICE" },
                    { "e11f4f6b-cd98-44ff-a176-a741b8709f8c", null, "Admin", "ADMIN" }
                });
        }
    }
}
