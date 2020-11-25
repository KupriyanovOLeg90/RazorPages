using RazorPagesLessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorPagesLessons.Services
{
    public class MockEmployeRepository : IEmployeRepository
    {
        private List<Employe> _employes;

        public MockEmployeRepository()
        {
            _employes = new List<Employe>() {
                new Employe() { Id = 0, Department = Dept.HR, Email = "Test@test.ti", Name = "Ivanov", PhotoPath = "avatar.png"  },
                new Employe() { Id = 1, Department = Dept.IT, Email = "asdasd@test.ti", Name = "Петров", PhotoPath = "avatar2.png"  },
                new Employe() { Id = 2, Department = Dept.Payrol, Email = "ыывв@test.ti", Name = "Ибрагимов", PhotoPath = "avatar3.png"  },
                new Employe() { Id = 3, Department = Dept.None, Email = "sds@test.ti", Name = "Медведева", PhotoPath = "avatar4.png"  },
                new Employe() { Id = 4, Department = Dept.IT, Email = "fff@sdd.ti", Name = "Новикова",  },
            };
        }


        public Employe GetEmployee(int Id)
        {
            return _employes.FirstOrDefault(x => x.Id == Id);
        }

        public Employe Add(Employe newEmploye)
        {
            newEmploye.Id = _employes.Max(x => x.Id) + 1;
            _employes.Add(newEmploye);

            return newEmploye;
        }


        public Employe Update(Employe updateEmploye)
        {
            var employe = _employes.FirstOrDefault(x => x.Id == updateEmploye.Id);

            if (employe != null)
            {
                employe.Name = updateEmploye.Name;
                employe.Email = updateEmploye.Email;
                employe.Department = updateEmploye.Department;
                employe.PhotoPath = updateEmploye.PhotoPath;
            }

            return employe;
        }

        IEnumerable<Employe> IEmployeRepository.GetAllEmpoyes()
        {
            return _employes;
        }

        public Employe Delete(int Id)
        {
            var employeTpDelete = _employes.FirstOrDefault(x => x.Id == Id);
            if (employeTpDelete != null)
            {
                _employes.Remove(employeTpDelete);
            }
            return employeTpDelete;
        }

        public IEnumerable<DepartmentHeadCount> EmployeCountByDept(Dept? dept)
        {
            IEnumerable<Employe> query = _employes;

            if (dept.HasValue)
                query = query.Where(x => x.Department.Value == dept.Value);

            return query.GroupBy(x => x.Department).
                        Select(x => new DepartmentHeadCount()
                        {
                            Department = x.Key.Value,
                            Count = x.Count()
                        }).ToList();
        }

        public IEnumerable<Employe> SearchEmpoyes(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _employes;

            return _employes.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) 
                                    || x.Email.ToLower().Contains(searchTerm.ToLower())).ToList();
        }
    }
}
