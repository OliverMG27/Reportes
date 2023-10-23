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

            // connect to database and check the user credentials
            try
            {
                string connectionString = "Data Source=10.1.0.11;Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;TrustServerCertificate=true";
                System.Data.SqlClient.SqlConnection connection = new(connectionString);
                {
                    connection.Open();
                    string sql = "SELECT * FROM Usuarios";

                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@Usuario", Usuario);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                         {
                            int Id = reader.GetInt32(0);
                            string Usuario = reader.GetString(1);
                            string Nombre = reader.GetString(2);
                            string Clave = reader.GetString(3);
                            string Correo = reader.GetString(4);
                            string Tipo = reader.GetString(5);
                            string hashedPassword = reader.GetString(6);
                       

                            // verify the password
                            var passwordHasher = new PasswordHasher<IdentityUser>();
                            var result = passwordHasher.VerifyHashedPassword(new IdentityUser(),
                                hashedPassword, Clave);

                            if (result == PasswordVerificationResult.Success
                                || result == PasswordVerificationResult.SuccessRehashNeeded)
                            {
                                // successful password verification => initialize the session
                                HttpContext.Session.SetInt32("Id", Id);
                                HttpContext.Session.SetString("Usuario", Usuario);
                                HttpContext.Session.SetString("Nombre", Nombre);
                
                   

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
            //wrong Email or password
            errorMessage = "El usuario y Clave son Incorrectos";
        }
    }
}