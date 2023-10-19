using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;



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
        }
    }
}