using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_scale",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    user_id = table.Column<Guid>(nullable: false),
                    weight = table.Column<float>(nullable: false),
                    Cheest = table.Column<int>(nullable: false),
                    waist = table.Column<int>(nullable: false),
                    upper_arm = table.Column<int>(nullable: false),
                    hip = table.Column<int>(nullable: false),
                    leg = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_scale", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_scale_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_scale_user_id",
                table: "user_scale",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_scale");
        }
    }
}
