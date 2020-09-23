using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "content",
                newName: "file");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "content",
                newName: "content_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file",
                table: "content",
                newName: "File");

            migrationBuilder.RenameColumn(
                name: "content_type",
                table: "content",
                newName: "ContentType");
        }
    }
}
