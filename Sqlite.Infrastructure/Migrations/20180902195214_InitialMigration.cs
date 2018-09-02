using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sqlite.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: true),
                    AddedOn = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseTypes");
        }
    }
}
