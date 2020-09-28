using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_content_delivery_type",
                table: "user_content");

            migrationBuilder.DropColumn(
                name: "delivery_type",
                table: "user_content");

            migrationBuilder.AddColumn<int>(
                name: "content_type",
                table: "content",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content_type",
                table: "content");

            migrationBuilder.AddColumn<string>(
                name: "delivery_type",
                table: "user_content",
                type: "varchar(16)",
                nullable: false,
                defaultValue: "Optional");

            migrationBuilder.CreateIndex(
                name: "IX_user_content_delivery_type",
                table: "user_content",
                column: "delivery_type")
                .Annotation("Npgsql:IndexMethod", "hash");
        }
    }
}
