using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEducatorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__courseapplies__educators_EducatorId",
                table: "_courseapplies");

            migrationBuilder.DropIndex(
                name: "IX__courseapplies_EducatorId",
                table: "_courseapplies");

            migrationBuilder.DropColumn(
                name: "EducatorId",
                table: "_courseapplies");

            migrationBuilder.AlterColumn<string>(
                name: "EducatorLastName",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EducatorFirstName",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EducatorEmail",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EducatorLastName",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EducatorFirstName",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EducatorEmail",
                table: "_educators",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EducatorId",
                table: "_courseapplies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__courseapplies_EducatorId",
                table: "_courseapplies",
                column: "EducatorId");

            migrationBuilder.AddForeignKey(
                name: "FK__courseapplies__educators_EducatorId",
                table: "_courseapplies",
                column: "EducatorId",
                principalTable: "_educators",
                principalColumn: "EducatorId");
        }
    }
}
