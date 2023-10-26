using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Reportes.Pages.Auth
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        [Required(ErrorMessage = "*Se requiere el Usuario")]
        public string Usuario { get; set; } = "";

        [Required(ErrorMessage = "*Se requiere la Clave")]
        public string Clave { get; set; } = "";

        public string errorMessage = "";
        public string successMessage = "";

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            if (HttpContext.Session.GetString("role") != null)
            {
                context.Result = new RedirectResult("/");
            }
        }

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
                string connectionString = "";
                System.Data.SqlClient.SqlConnection connection = new(connectionString);
                {
                    connection.Open();
                    string sql = "SELECT * FROM users WHERE email=@email";

                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@email", Usuario);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string firstname = reader.GetString(1);
                            string lastname = reader.GetString(2);
                            string email = reader.GetString(3);
                            string phone = reader.GetString(4);
                            string address = reader.GetString(5);
                            string hashedPassword = reader.GetString(6);
                            string role = reader.GetString(7);
                            string created_at = reader.GetDateTime(8).ToString("MM/dd/yyyy");

                            // verify the password
                            var passwordHasher = new PasswordHasher<IdentityUser>();
                            var result = passwordHasher.VerifyHashedPassword(new IdentityUser(),
                                hashedPassword, Clave);

                            if (result == PasswordVerificationResult.Success
                                || result == PasswordVerificationResult.SuccessRehashNeeded)
                            {
                                // successful password verification => initialize the session
                                HttpContext.Session.SetInt32("id", id);
                                HttpContext.Session.SetString("firstname", firstname);
                                HttpContext.Session.SetString("lastname", lastname);
                                HttpContext.Session.SetString("email", email);
                                HttpContext.Session.SetString("phone", phone);
                                HttpContext.Session.SetString("address", address);
                                HttpContext.Session.SetString("role", role);
                                HttpContext.Session.SetString("created_at", created_at);

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
            errorMessage = "El suario y Clave son Incorrectos";
        }
    }
}