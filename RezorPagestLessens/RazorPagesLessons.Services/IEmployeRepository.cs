using RazorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RazorPagesLessons.Services
{
    public interface IEmployeRepository
    {
        IEnumerable<Employe> SearchEmpoyes(string searchTerm);

        IEnumerable<Employe> GetAllEmpoyes();

        Employe GetEmployee(int Id);

        Employe Update(Employe updateEmploye);

        Employe Add(Employe newEmploye);

        Employe Delete(int Id);

        IEnumerable<DepartmentHeadCount> EmployeCountByDept(Dept? dept);
    }
}
