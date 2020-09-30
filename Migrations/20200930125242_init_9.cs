using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dytsenayasar.Migrations
{
    public partial class init_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_Users_creator_id",
                table: "content");

            migrationBuilder.DropTable(
                name: "content_category");

            migrationBuilder.DropTable(
                name: "user_content");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropIndex(
                name: "IX_content_creator_id",
                table: "content");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "content");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "content",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_content_UserID",
                table: "content",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_content_Users_UserID",
                table: "content",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_Users_UserID",
                table: "content");

            migrationBuilder.DropIndex(
                name: "IX_content_UserID",
                table: "content");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "content");

            migrationBuilder.AddColumn<Guid>(
                name: "creator_id",
                table: "content",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_en = table.Column<string>(type: "varchar(64)", nullable: false),
                    name_tr = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_content",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    content_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    validity_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_content_content_content_id",
                        column: x => x.content_id,
                        principalTable: "content",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_content_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    content_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_content_category_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_content_category_content_content_id",
                        column: x => x.content_id,
                        principalTable: "content",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_content_creator_id",
                table: "content",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_category_category_id",
                table: "content_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_content_category_content_id",
                table: "content_category",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_content_content_id",
                table: "user_content",
                column: "content_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_content_user_id",
                table: "user_content",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_content_Users_creator_id",
                table: "content",
                column: "creator_id",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
