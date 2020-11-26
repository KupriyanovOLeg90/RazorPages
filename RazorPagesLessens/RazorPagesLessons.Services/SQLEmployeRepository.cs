using Microsoft.EntityFrameworkCore;
using RazorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesLessons.Services
{
    public class SQLEmployeRepository : IEmployeRepository
    {
        public AppDbContext _context { get; }
        public SQLEmployeRepository(AppDbContext context)
        {
            _context = context;
        }

        //public Employe Add(Employe newEmploye)
        //{
        //    _context.Employes.Add(newEmploye);
        //    _context.SaveChanges();

        //    return newEmploye;
        //}

        public Employe Add(Employe newEmploye)
        {
            _context.Database.ExecuteSqlRaw("spAddNewEmployee {0}, {1}, {2}, {3}", 
                newEmploye.Name, newEmploye.Email, newEmploye.PhotoPath, newEmploye.Department);

            return newEmploye;
        }

        public Employe Delete(int Id)
        {
            var employeTpDelete = _context.Employes.Find(Id);
            if (employeTpDelete != null)
            {
                _context.Employes.Remove(employeTpDelete);
                _context.SaveChanges();
            }
            return employeTpDelete;
        }

        public IEnumerable<DepartmentHeadCount> EmployeCountByDept(Dept? dept)
        {
            IEnumerable<Employe> query = _context.Employes;

            if (dept.HasValue)
                query = query.Where(x => x.Department.Value == dept.Value);

            return query.GroupBy(x => x.Department).
                        Select(x => new DepartmentHeadCount()
                        {
                            Department = x.Key.Value,
                            Count = x.Count()
                        }).ToList();
        }

        public IEnumerable<Employe> GetAllEmpoyes()
        {
            //return _context.Employes;

            return _context.Employes
                    .FromSqlRaw<Employe>("Select * from Employees").ToList();
        }

        public Employe GetEmployee(int Id)
        {
            //return _context.Employes.FirstOrDefault(x => x.Id == Id);

            return _context.Employes
                    .FromSqlRaw<Employe>("CodeFirstSpGetEmployeeById {0}", Id)
                    .ToList().FirstOrDefault();
        }

        public IEnumerable<Employe> SearchEmpoyes(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _context.Employes;

            return _context.Employes.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())
                                    || x.Email.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public Employe Update(Employe updateEmploye)
        {
            var employe = _context.Employes.Attach(updateEmploye);
            employe.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

            return updateEmploye;
        }
    }
}
