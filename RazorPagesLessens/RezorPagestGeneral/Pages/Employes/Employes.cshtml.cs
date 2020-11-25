using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLessons.Services;
using RazorPagesLessons.Models;

namespace RezorPagestGeneral.Pages.Employes
{
    public class EmployesModel : PageModel
    {
        private readonly IEmployeRepository _db;
        public EmployesModel(IEmployeRepository db)
        {
            this._db = db;
        }

        public IEnumerable<Employe> Employes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            Employes = _db.SearchEmpoyes(SearchTerm);
        }
    }
}
