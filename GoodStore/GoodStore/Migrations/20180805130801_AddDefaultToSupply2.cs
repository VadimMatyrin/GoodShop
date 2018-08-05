using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodStore.Migrations
{
    public partial class AddDefaultToSupply2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Supplies",
                nullable: false,
                computedColumnSql: "GETDATE()",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Supplies",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldComputedColumnSql: "GETDATE()");
        }
    }
}
