using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class CourseEducatorMandatory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__courses__educators_EducatorId",
                table: "_courses");

            migrationBuilder.AlterColumn<int>(
                name: "EducatorId",
                table: "_courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__courses__educators_EducatorId",
                table: "_courses",
                column: "EducatorId",
                principalTable: "_educators",
                principalColumn: "EducatorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__courses__educators_EducatorId",
                table: "_courses");

            migrationBuilder.AlterColumn<int>(
                name: "EducatorId",
                table: "_courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__courses__educators_EducatorId",
                table: "_courses",
                column: "EducatorId",
                principalTable: "_educators",
                principalColumn: "EducatorId");
        }
    }
}
