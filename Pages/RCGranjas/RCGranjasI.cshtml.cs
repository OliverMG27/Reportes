using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Web.Mvc;

using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace Reportes.Pages.RCGranjas
{
    public class RCGranjasIModel : PageModel
    {

        [BindProperty]
        public int SelectedEmpresaId { get; set; }

        [BindProperty]
        public int SelectedGranjaId { get; set; }

        public List<SelectListItem> Empresas { get; set; }
        public List<SelectListItem> Granjas { get; set; }

        public void OnGet()
        {
            CargarEmpresas();
            Granjas = new List<SelectListItem>();
        }

        public void OnPost()
        {
            CargarEmpresas();
            CargarGranjas(SelectedEmpresaId);
        }

        private void CargarEmpresas()
        {
            // Utiliza tu cadena de conexión y consulta SQL para obtener las empresas
            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();
                string sqlQueryEmpresa = "SELECT * FROM Empresas ORDER BY razonSocial";
                using (var command = new SqlCommand(sqlQueryEmpresa, connection))
                using (var reader = command.ExecuteReader())
                {
                    Empresas = new List<SelectListItem>
                {
                    new  SelectListItem { Value = "", Text = " - - - - Selecciona una empresa - - - - " }
                };

                    while (reader.Read())
                    {
                        Empresas.Add(new SelectListItem
                        {
                            Value = reader["id"].ToString(),
                            Text = reader["razonSocial"].ToString()
                        });
                    }
                }
            }
        }

        private void CargarGranjas(int empresaId)
        {
            // Utiliza tu cadena de conexión y consulta SQL para obtener las granjas de la empresa seleccionada
            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();
                string sqlQueryGranjas = $"SELECT idEmpresa,granja FROM Granjas WHERE IdEmpresa = {empresaId} AND Activa = 'S' ORDER BY granja";

                using (var command = new SqlCommand(sqlQueryGranjas, connection))
                using (var reader = command.ExecuteReader())
                {
                    Granjas = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = " - - - - Selecciona una granja - - - - " }
                };

                    while (reader.Read())
                    {
                        Granjas.Add(new SelectListItem
                        {
                            Value = reader["idEmpresa"].ToString(),
                            Text = reader["granja"].ToString().ToUpper()
                        });
                    }
                }
            }
        }
    }
}
