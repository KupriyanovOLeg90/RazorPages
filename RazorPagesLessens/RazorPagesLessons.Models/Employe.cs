using System.ComponentModel.DataAnnotations;

namespace RazorPagesLessons.Models
{
    public class Employe
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The name field can not be null. Plaese write the name.")]
        [MaxLength(50), MinLength(2)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Не корректрый Email")]
        [MaxLength(50), MinLength(5)]
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        public Dept? Department { get; set; }
    }
}
