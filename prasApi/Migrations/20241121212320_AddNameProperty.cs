using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Name",
                table: "AspNetUsers");

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
    }
}
