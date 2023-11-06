using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Reportes.MyHelpers;

namespace Reportes.Pages
{

    public class IndexModel : PageModel
    {
        public IActionResult Index()
        {
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "*Se requiere el Usuario")]
        public string Usuario { get; set; } = "";
        [BindProperty]
        [DataType(DataType.Password)]
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
            try
            {
                string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
        
                System.Data.SqlClient.SqlConnection connection = new(connectionString);
                {
                    connection.Open();
                    string sql = "SELECT * FROM Usuarios WHERE Usuario=@Usuario AND Clave = @Clave";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", Usuario);
                        command.Parameters.AddWithValue("@Clave", Clave);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int Id = reader.GetInt32(0);
                                string Nombre = reader.GetString(1);
                                string Correo = reader.GetString(2);
                                string Tipo = reader.GetString(3);
                                string Activo = reader.GetString(4);
         
                                // the user is authenticated successfully => redirect to the home page
                                Response.Redirect("/Menu/MenuI");
                                

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Wrong Email or Password
            errorMessage = "El Usuario o Contraseña no son validos";
        }
    }
}

