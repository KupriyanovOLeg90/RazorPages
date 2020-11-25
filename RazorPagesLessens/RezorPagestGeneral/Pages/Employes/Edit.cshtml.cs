using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLessons.Services;
using RazorPagesLessons.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace RazorPagestGeneral.Pages.Employes
{
    public class EditModel : PageModel
    {
        private readonly IEmployeRepository _employeRepository;
        public IWebHostEnvironment _WebHostEnviroment { get; }
        public EditModel(IEmployeRepository employeRepository, IWebHostEnvironment webHostEnviroment)
        {
            _employeRepository = employeRepository;
            _WebHostEnviroment = webHostEnviroment;
        }

        [BindProperty]
        public Employe Employee { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet(int? Id)
        {
            Employee = !Id.HasValue ? new Employe() : _employeRepository.GetEmployee(Id.Value);

            if (Employee == null)
                return RedirectToPage("/NotFound");
            else
                return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string FilePath = Path.Combine(_WebHostEnviroment.WebRootPath, "images", Employee.PhotoPath);
                        
                        if(Employee.PhotoPath != "noimage.png")
                        System.IO.File.Delete(FilePath);
                    }

                    Employee.PhotoPath = ProcessUploadedFile();
                }

                if (Employee.Id == 0)
                {
                    _employeRepository.Add(Employee);
                    TempData["SuccessMessage"] = $"Adding {Employee.Name} successful";
                }
                else
                {
                    _employeRepository.Update(Employee);
                    TempData["SuccessMessage"] = $"Update {Employee.Name} successful";
                }



                return RedirectToPage("Employes");
            }


            return Page();
        }

        public void OnPostUpdateNotifycationReference(int? Id)
        {
            Employee = !Id.HasValue ? new Employe() : _employeRepository.GetEmployee(Id.Value);
            if (this.Notify)
                this.Message = "Thank you for turning on notifications";
            else
                this.Message = "You have turned off Emails notifications";
        }




        private string ProcessUploadedFile()
        {
            string uniqFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_WebHostEnviroment.WebRootPath, "images");
                uniqFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqFileName;
        }


    }
}
