using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "user");

            migrationBuilder.DropColumn(
                name: "image",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "image",
                table: "user",
                type: "uuid",
                nullable: true);
        }
    }
}
