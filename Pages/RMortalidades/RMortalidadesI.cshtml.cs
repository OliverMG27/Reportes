
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Web.Mvc;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using Reportes.Models;


namespace Reportes.Pages.RMortalidades
{
    public class RMortalidadesIModel : PageModel
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

        private void MortalidadSemana(int IdEmpresa, DateTime FechaHasta, string Granja)
        {
            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();
                string sqlQueryReporte = $@"SELECT T0.IdEmpresa,(SELECT T1.RazonSocial FROM Empresas T1
                                           WHERE T1.Id = T0.IdEmpresa) AS RazonSocial,T0.Granja,T0.FechaMovimiento,T0.Lote,T0.Nave,
                                           SUM(T0.Cantidad) AS Cantidad
                                           FROM DetalleCerdo T0
                                           WHERE T0.Motivo <> 'Traspaso a Engorda'
                                           AND T0.Tipo = 'E'
                                           AND T0.IdEmpresa = @IdEmpresa
                                           AND T0.FechaMovimiento <= @Hasta
                                           (UPPER ( @CmbGranja ) = 'ALL' OR T0.Granja = @CmbGranja )
                                           GROUP BY T0.IdEmpresa, T0.Granja, T0.Lote, T0.FechaMovimiento, T0.Nave";

                using (var command = new SqlCommand(sqlQueryReporte, connection))
                {

                    command.Parameters.AddWithValue("@IdEmpresa",IdEmpresa);
                    command.Parameters.AddWithValue("@Hasta", FechaHasta.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@CmbGranja", Granja.ToUpper());


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Acceder a los resultados
                            var idEmpresa = reader["IdEmpresa"];
                            var razonSocial = reader["RazonSocial"];
                        }    // Otros campos...
                    }
                }
            }
        }
    }
}
/*comentario de prueba*/