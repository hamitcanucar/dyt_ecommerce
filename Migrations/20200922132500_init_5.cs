using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dytsenayasar.Migrations
{
    public partial class init_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age_limit",
                table: "content");

            migrationBuilder.DropColumn(
                name: "base_price",
                table: "content");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Image",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "content",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "File",
                table: "content",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
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
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    user_id = table.Column<Guid>(nullable: false),
                    delivery_type = table.Column<string>(type: "varchar(16)", nullable: false, defaultValue: "Optional"),
                    validity_date = table.Column<DateTime>(nullable: false),
                    content_id = table.Column<Guid>(nullable: false)
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
                name: "user_form",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    UserId = table.Column<Guid>(nullable: false),
                    DietType = table.Column<int>(nullable: false),
                    Job = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    CallTimes = table.Column<string>(nullable: true),
                    References = table.Column<string>(nullable: true),
                    WakeUpTime = table.Column<string>(nullable: true),
                    BreakfastTime = table.Column<string>(nullable: true),
                    LunchTime = table.Column<string>(nullable: true),
                    DinnerTime = table.Column<string>(nullable: true),
                    SleepTime = table.Column<string>(nullable: true),
                    BreakfastFoods = table.Column<string>(nullable: true),
                    LunchFoods = table.Column<string>(nullable: true),
                    DinnerFoods = table.Column<string>(nullable: true),
                    FoodsUntilSleep = table.Column<string>(nullable: true),
                    SleepPatterns = table.Column<string>(nullable: true),
                    Drugs = table.Column<string>(nullable: true),
                    Sports = table.Column<string>(nullable: true),
                    BadHabits = table.Column<string>(nullable: true),
                    ToiletFrequency = table.Column<string>(nullable: true),
                    AllergyFoods = table.Column<string>(nullable: true),
                    BestFoods = table.Column<string>(nullable: true),
                    FavoriteBreakfastFoods = table.Column<string>(nullable: true),
                    FavoriteVegetablesFoods = table.Column<string>(nullable: true),
                    FavoriteMeatFoods = table.Column<string>(nullable: true),
                    FavoriteFruits = table.Column<string>(nullable: true),
                    OralDiseases = table.Column<string>(nullable: true),
                    CardiovascularDiseases = table.Column<string>(nullable: true),
                    StomachAndIntestineDiseases = table.Column<string>(nullable: true),
                    ThyroidDiseases = table.Column<string>(nullable: true),
                    Anemia = table.Column<string>(nullable: true),
                    UrinaryInfection = table.Column<string>(nullable: true),
                    LungInfection = table.Column<string>(nullable: true),
                    ContinuousDrugs = table.Column<string>(nullable: true),
                    Diabetes = table.Column<string>(nullable: true),
                    Hospital = table.Column<string>(nullable: true),
                    Operation = table.Column<string>(nullable: true),
                    IsRegl = table.Column<bool>(nullable: true),
                    IsOrderRegl = table.Column<bool>(nullable: true),
                    Parturition = table.Column<string>(nullable: true),
                    Breastfeed = table.Column<string>(nullable: true),
                    Weight = table.Column<float>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Chest = table.Column<int>(nullable: false),
                    Waist = table.Column<int>(nullable: false),
                    MaxWeight = table.Column<float>(nullable: false),
                    MinWeight = table.Column<float>(nullable: false),
                    Methods = table.Column<string>(nullable: true),
                    Family = table.Column<string>(nullable: true),
                    NoteToDietitian = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_form", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_form_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_category",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(nullable: false, defaultValueSql: "now()"),
                    content_id = table.Column<Guid>(nullable: false),
                    category_id = table.Column<int>(nullable: false)
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
                name: "IX_user_content_delivery_type",
                table: "user_content",
                column: "delivery_type")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.CreateIndex(
                name: "IX_user_content_user_id",
                table: "user_content",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_form_UserId",
                table: "user_form",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content_category");

            migrationBuilder.DropTable(
                name: "user_content");

            migrationBuilder.DropTable(
                name: "user_form");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "content");

            migrationBuilder.DropColumn(
                name: "File",
                table: "content");

            migrationBuilder.AddColumn<int>(
                name: "age_limit",
                table: "content",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "base_price",
                table: "content",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
