using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reportes.Pages.Pruebas
{
    public class PruebasIModel : PageModel
    {

    
        public void OnGet()
        {
            // L�gica para manejar la solicitud GET
        }


        [HttpPost]
        public IActionResult OnPost(string fechaInicio, string fechaFin)
        {
            // L�gica para manejar la solicitud POST con las fechas
            // ...

            // Devolver una respuesta (puede ser un objeto JSON)
            return new JsonResult(new { mensaje = "Actualizaci�n exitosa" });
        }

    }
}
