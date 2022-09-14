using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jwtauthentication6._0.Migrations
{
    public partial class UpdateDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshToken",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RefreshToken",
                keyColumn: "Token",
                keyValue: null,
                column: "Token",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshToken",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
