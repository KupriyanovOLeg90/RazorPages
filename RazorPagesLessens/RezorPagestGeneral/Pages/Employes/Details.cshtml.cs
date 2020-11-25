
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLessons.Services;
using RazorPagesLessons.Models;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagestGeneral.Pages.Employes
{
    public class DetailsModel : PageModel
    {
        public IEmployeRepository _employeRepository;        
       
        public DetailsModel(IEmployeRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        public Employe Employee { get; private set; }

        public IActionResult OnGet(int Id)
        {
            Employee = _employeRepository.GetEmployee(Id);

            if (Employee == null)
                return RedirectToPage("/NotFound");
            else
                return Page();
        }
    }
}
