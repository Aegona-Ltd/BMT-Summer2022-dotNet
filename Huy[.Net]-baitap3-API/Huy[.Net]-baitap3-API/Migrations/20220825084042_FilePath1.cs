using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huy_.Net__baitap3_API.Migrations
{
    public partial class FilePath1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Contact",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Contact",
                newName: "File");
        }
    }
}
