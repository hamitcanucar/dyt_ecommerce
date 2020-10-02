using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_user_user_id",
                table: "content");

            migrationBuilder.DropPrimaryKey(
                name: "PK_content",
                table: "content");

            migrationBuilder.RenameTable(
                name: "content",
                newName: "user_file");

            migrationBuilder.RenameIndex(
                name: "IX_content_user_id",
                table: "user_file",
                newName: "IX_user_file_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_file",
                table: "user_file",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_file_user_user_id",
                table: "user_file",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_file_user_user_id",
                table: "user_file");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_file",
                table: "user_file");

            migrationBuilder.RenameTable(
                name: "user_file",
                newName: "content");

            migrationBuilder.RenameIndex(
                name: "IX_user_file_user_id",
                table: "content",
                newName: "IX_content_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_content",
                table: "content",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_content_user_user_id",
                table: "content",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
