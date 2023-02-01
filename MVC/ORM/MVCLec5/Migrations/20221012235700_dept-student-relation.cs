using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLec5.Migrations
{
    public partial class deptstudentrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptNo",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeptNo",
                table: "Students",
                column: "DeptNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_dept_DeptNo",
                table: "Students",
                column: "DeptNo",
                principalTable: "dept",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_dept_DeptNo",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeptNo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeptNo",
                table: "Students");
        }
    }
}
