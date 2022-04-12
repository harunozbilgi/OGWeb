using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGWeb.Infrastructure.Migrations
{
    public partial class Initial_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppSeoCode",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "IsDynamic",
                table: "AppSeos");

            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "AppSeos");

            migrationBuilder.AddColumn<string>(
                name: "Description_Seo",
                table: "Works",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Keyword_Seo",
                table: "Works",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_Seo",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "Keyword_Seo",
                table: "Works");

            migrationBuilder.AddColumn<string>(
                name: "AppSeoCode",
                table: "Works",
                type: "varchar(15)",
                maxLength: 15,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsDynamic",
                table: "AppSeos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "AppSeos",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
