using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Reportes.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        [Required(ErrorMessage = "*Se requiere el Usuario")]
        public string Usuario { get; set; } = "";

        [Required(ErrorMessage = "*Se requiere la Clave")]
        public string Clave { get; set; } = "";

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Datos Invalidos";
                return;
            }

            // successfull data validation

            // connect to database and check the user credentials

        }
    }
}