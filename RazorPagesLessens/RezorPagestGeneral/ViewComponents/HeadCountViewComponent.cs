using Microsoft.AspNetCore.Mvc;
using RazorPagesLessons.Models;
using RazorPagesLessons.Services;

namespace RazorPagestGeneral.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        public IEmployeRepository _employeRepository { get; }
        public HeadCountViewComponent(IEmployeRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        public IViewComponentResult Invoke(Dept? dept = null) 
        {
            var result = _employeRepository.EmployeCountByDept(dept);

            return View(result);
        }
    }
}
