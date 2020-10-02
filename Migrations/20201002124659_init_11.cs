using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_files",
                table: "content");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "content",
                newName: "file_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "content",
                newName: "name");

            migrationBuilder.AddColumn<byte[]>(
                name: "data_files",
                table: "content",
                type: "bytea",
                nullable: true);
        }
    }
}
