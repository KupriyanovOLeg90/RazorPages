using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPagesLessons.Services.Migrations
{
    public partial class AddNewEployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spAddNewEmployee 
                                   @Name nvarchar(100),
                                   @Email nvarchar(100),
                                   @PhotoPath nvarchar(max),
                                   @Department int
                                 as
                                    Begin
                                        Insert INTO Employes (Name, Email, PhotoPath, Department)
                                        values(@Name, @Email, @PhotoPath, @Department)
                                    end"
                                 ;

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"drop procedure spAddNewEmployee ";
            migrationBuilder.Sql(procedure);
        }
    }
}
