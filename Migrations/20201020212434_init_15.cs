using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_gender",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_personal_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "birth_day",
                table: "user");

            migrationBuilder.DropColumn(
                name: "City",
                table: "user");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "user");

            migrationBuilder.DropColumn(
                name: "personal_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "user");

            migrationBuilder.CreateTable(
                name: "user_information",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    personal_id = table.Column<string>(type: "varchar(64)", nullable: false),
                    birth_day = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    phone = table.Column<string>(type: "varchar(32)", nullable: true),
                    gender = table.Column<string>(type: "varchar(16)", nullable: false, defaultValue: "Male"),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_information", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_information_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_information_gender",
                table: "user_information",
                column: "gender")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.CreateIndex(
                name: "IX_user_information_UserId",
                table: "user_information",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_information");

            migrationBuilder.AddColumn<DateTime>(
                name: "birth_day",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "user",
                type: "varchar(16)",
                nullable: false,
                defaultValue: "Male");

            migrationBuilder.AddColumn<string>(
                name: "personal_id",
                table: "user",
                type: "varchar(64)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "user",
                type: "varchar(32)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_gender",
                table: "user",
                column: "gender")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.CreateIndex(
                name: "IX_user_personal_id",
                table: "user",
                column: "personal_id",
                unique: true);
        }
    }
}
