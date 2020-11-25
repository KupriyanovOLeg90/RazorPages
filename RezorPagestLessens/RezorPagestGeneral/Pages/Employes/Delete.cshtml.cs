using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLessons.Services;
using RazorPagesLessons.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RazorPagestGeneral.Pages.Employes
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public DeleteModel(IEmployeRepository employeRepository, IWebHostEnvironment webHostEnviroment)
        {
            _employeRepository = employeRepository;
            _webHostEnviroment = webHostEnviroment;
        }

        [BindProperty]
        public Employe Employee { get; set; }
        public IActionResult OnGet(int Id)
        {
            Employee = _employeRepository.GetEmployee(Id);

            if (Employee == null)
                return RedirectToPage("NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            Employe deletedEmploe = _employeRepository.Delete(Employee.Id);

            if (deletedEmploe.PhotoPath != null)
            {
                string FilePath = Path.Combine(_webHostEnviroment.WebRootPath, "images", deletedEmploe.PhotoPath);

                if (deletedEmploe.PhotoPath != "noimage.png")
                    System.IO.File.Delete(FilePath);
            }


            if (deletedEmploe == null)
                return RedirectToPage("NotFound");
            

            return RedirectToPage("Employes");
        }
    }
}
