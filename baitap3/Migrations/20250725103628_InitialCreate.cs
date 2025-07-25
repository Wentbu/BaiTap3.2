using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace baitap3.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    AccessProperties = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InternName = table.Column<string>(type: "text", nullable: true),
                    InternAddress = table.Column<string>(type: "text", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InternMail = table.Column<string>(type: "text", nullable: true),
                    InternMailReplace = table.Column<string>(type: "text", nullable: true),
                    University = table.Column<string>(type: "text", nullable: true),
                    CitizenIdentification = table.Column<string>(type: "text", nullable: true),
                    CitizenIdentificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Major = table.Column<string>(type: "text", nullable: true),
                    Internable = table.Column<bool>(type: "boolean", nullable: true),
                    FullTime = table.Column<bool>(type: "boolean", nullable: true),
                    CvFile = table.Column<string>(type: "text", nullable: true),
                    InternSpecialized = table.Column<int>(type: "integer", nullable: true),
                    TelephoneNum = table.Column<string>(type: "text", nullable: true),
                    InternStatus = table.Column<string>(type: "text", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HowToKnowAlta = table.Column<string>(type: "text", nullable: true),
                    InternPassword = table.Column<string>(type: "text", nullable: true),
                    ForeignLanguage = table.Column<string>(type: "text", nullable: true),
                    YearOfExperiences = table.Column<int>(type: "integer", nullable: true),
                    PasswordStatus = table.Column<string>(type: "text", nullable: true),
                    ReadyToWork = table.Column<bool>(type: "boolean", nullable: true),
                    InternEnabled = table.Column<bool>(type: "boolean", nullable: true),
                    EntranceTest = table.Column<string>(type: "text", nullable: true),
                    Introduction = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    LinkProduct = table.Column<string>(type: "text", nullable: true),
                    JobFields = table.Column<string>(type: "text", nullable: true),
                    HiddenToEnterprise = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllowAccesses",
                columns: new[] { "Id", "AccessProperties", "RoleId", "TableName" },
                values: new object[,]
                {
                    { 1, "Id,InternName,InternAddress,ImageData,DateOfBirth,InternMail,InternMailReplace,University,CitizenIdentification,CitizenIdentificationDate,Major,Internable,FullTime,Cvfile,InternSpecialized,TelephoneNum,InternStatus,RegisteredDate,HowToKnowAlta,InternPassword,ForeignLanguage,YearOfExperiences,PasswordStatus,ReadyToWork,InternEnabled,EntranceTest,Introduction,Note,LinkProduct,JobFields,HiddenToEnterprise", 1, "Intern" },
                    { 2, "InternMail,InternName,Major", 2, "Intern" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "EnterpriseUser" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateOfBirth", "FullName", "PasswordHash", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin User", "$2a$11$51d40/uKZqVvw.cGyI3qZOjTnJE58EzY0mZj2rymv3WfnIi7FWP2S", 1, "admin" },
                    { 2, new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Nicky", "$2a$11$/ekRz6Ny8IQaH3GHQtxfb.VfuuJJBykiae7tTJCA7OtJ4Hhc2YMDq", 2, "Nick" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowAccesses");

            migrationBuilder.DropTable(
                name: "Interns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
