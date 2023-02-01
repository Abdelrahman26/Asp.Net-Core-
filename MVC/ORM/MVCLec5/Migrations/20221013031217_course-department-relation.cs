using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLec5.Migrations
{
    public partial class coursedepartmentrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDepartment",
                columns: table => new
                {
                    coursesCrs_Id = table.Column<int>(type: "int", nullable: false),
                    departmentsDeptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDepartment", x => new { x.coursesCrs_Id, x.departmentsDeptId });
                    table.ForeignKey(
                        name: "FK_CourseDepartment_Courses_coursesCrs_Id",
                        column: x => x.coursesCrs_Id,
                        principalTable: "Courses",
                        principalColumn: "Crs_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDepartment_dept_departmentsDeptId",
                        column: x => x.departmentsDeptId,
                        principalTable: "dept",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDepartment_departmentsDeptId",
                table: "CourseDepartment",
                column: "departmentsDeptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDepartment");
        }
    }
}
