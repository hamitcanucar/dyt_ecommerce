using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "content",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    title = table.Column<string>(type: "varchar(128)", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", nullable: true),
                    age_limit = table.Column<int>(nullable: false, defaultValue: 0),
                    base_price = table.Column<double>(nullable: false, defaultValue: 0.0),
                    image = table.Column<Guid>(nullable: true),
                    validity_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_content_Users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_content_creator_id",
                table: "content",
                column: "creator_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content");
        }
    }
}
