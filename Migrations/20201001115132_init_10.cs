using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dytsenayasar.Migrations
{
    public partial class init_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_Users_UserID",
                table: "content");

            migrationBuilder.DropForeignKey(
                name: "FK_user_client_Users_user_id",
                table: "user_client");

            migrationBuilder.DropForeignKey(
                name: "FK_user_form_Users_UserId",
                table: "user_form");

            migrationBuilder.DropForeignKey(
                name: "FK_user_request_Users_user_id",
                table: "user_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "content_type",
                table: "content");

            migrationBuilder.DropColumn(
                name: "file",
                table: "content");

            migrationBuilder.DropColumn(
                name: "image",
                table: "content");

            migrationBuilder.DropColumn(
                name: "validity_date",
                table: "content");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "content",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "content",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "content",
                newName: "file_type");

            migrationBuilder.RenameIndex(
                name: "IX_content_UserID",
                table: "content",
                newName: "IX_content_user_id");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "user",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "user",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "user",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "user",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "user",
                newName: "user_type");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "user",
                newName: "personal_id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "user",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "user",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "user",
                newName: "create_time");

            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "user",
                newName: "birth_day");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "content",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                table: "content",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "data_files",
                table: "content",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "user",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "user",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "user",
                type: "varchar(16)",
                nullable: false,
                defaultValue: "Male",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "user",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "user",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                table: "user",
                type: "varchar(16)",
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "personal_id",
                table: "user",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "user",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "user",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_time",
                table: "user",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type",
                table: "user",
                column: "user_type")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.AddForeignKey(
                name: "FK_content_user_user_id",
                table: "content",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_client_user_user_id",
                table: "user_client",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_form_user_UserId",
                table: "user_form",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_request_user_user_id",
                table: "user_request",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_user_user_id",
                table: "content");

            migrationBuilder.DropForeignKey(
                name: "FK_user_client_user_user_id",
                table: "user_client");

            migrationBuilder.DropForeignKey(
                name: "FK_user_form_user_UserId",
                table: "user_form");

            migrationBuilder.DropForeignKey(
                name: "FK_user_request_user_user_id",
                table: "user_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_email",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_gender",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_personal_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_user_type",
                table: "user");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "content");

            migrationBuilder.DropColumn(
                name: "data_files",
                table: "content");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "content",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "content",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "file_type",
                table: "content",
                newName: "description");

            migrationBuilder.RenameIndex(
                name: "IX_content_user_id",
                table: "content",
                newName: "IX_content_UserID");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Users",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "Users",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "user_type",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "personal_id",
                table: "Users",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "Users",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "birth_day",
                table: "Users",
                newName: "BirthDay");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "content",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "content_type",
                table: "content",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "file",
                table: "content",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "image",
                table: "content",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "validity_date",
                table: "content",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldDefaultValue: "Male");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldDefaultValue: "User");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalId",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_content_Users_UserID",
                table: "content",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_client_Users_user_id",
                table: "user_client",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_form_Users_UserId",
                table: "user_form",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_request_Users_user_id",
                table: "user_request",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
