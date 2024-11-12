using Microsoft.EntityFrameworkCore.Migrations;
using prasApi.Data;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedReportType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "149295af-251f-40c3-95b2-012531fdcc8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4df99368-d65a-403c-8205-f37e29ec3aeb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd510879-cf0d-4a49-8143-907fe1495991");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "149295af-251f-40c3-95b2-012531fdcc8e", null, "Admin", "ADMIN" },
                    { "4df99368-d65a-403c-8205-f37e29ec3aeb", null, "Police", "POLICE" },
                    { "bd510879-cf0d-4a49-8143-907fe1495991", null, "User", "USER" }
                });
        }

        internal static async Task SeedDataAsync(ApplicationDbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
