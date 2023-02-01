using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCLec5.Migrations
{
    public partial class initial : Migration
    {
        // function used to update database then, so far id does not update db
        // but just show you, what is gonna updated
        
        // to update database after declearing migrationm use this command
        // update-database
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students", // same name of db class property in ITIDB 
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        // when want to drop migration 
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
