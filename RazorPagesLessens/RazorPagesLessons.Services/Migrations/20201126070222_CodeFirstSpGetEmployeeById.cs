using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesLessons.Services.Migrations
{
    public partial class CodeFirstSpGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE CodeFirstSpGetEmployeeById
                                @id int
                                as 
                                Begin
	                                select * from Employes 
	                                where Id = @id
                                End";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE CodeFirstSpGetEmployeeById";

            migrationBuilder.Sql(procedure);
        }
    }
}
