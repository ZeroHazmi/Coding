using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToReportEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e4e27bb-5cab-4a20-898e-1fd2a0037119");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d29fd543-ef7d-4358-8ae2-24d5e300580e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e53eb653-3ffe-469c-9cf5-c7ce2bb22265");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "ReportDetail",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateStructure",
                table: "ReportTypes",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "json");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlineAllowed",
                table: "ReportTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reports",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "IcNumber",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FieldValue",
                table: "ReportDetail",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "json");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ReportDetail",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ReportDetail",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsOnlineAllowed",
                table: "ReportTypes");

            migrationBuilder.DropColumn(
                name: "IcNumber",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ReportDetail");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "ReportDetail",
                newName: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateStructure",
                table: "ReportTypes",
                type: "json",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FieldValue",
                table: "ReportDetail",
                type: "json",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e4e27bb-5cab-4a20-898e-1fd2a0037119", null, "User", "USER" },
                    { "d29fd543-ef7d-4358-8ae2-24d5e300580e", null, "Admin", "ADMIN" },
                    { "e53eb653-3ffe-469c-9cf5-c7ce2bb22265", null, "Police", "POLICE" }
                });
        }
    }
}
