using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Web.Mvc;

using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using Reportes.Models;
using Reportes.Controllers;
using DocumentFormat.OpenXml.EMMA;

namespace Reportes.Pages.RPesadas
{
    public class RPesadasIModel : PageModel
    {

        [BindProperty]
        public int SelectedEmpresaId { get; set; }

        [BindProperty]
        public int SelectedGranjaId { get; set; }

        [BindProperty]
        public DateTime FechaInicio { get; set; }

        [BindProperty]
        public DateTime FechaFin { get; set; }

      

        public List<SelectListItem> Empresas { get; set; }
        public List<SelectListItem> Granjas { get; set; }
        public List<SelectListItem> Pesadas { get; set; }


        public void OnGet()
        {
            CargarEmpresas();
            Granjas = new List<SelectListItem>();
            CargarGranjas(SelectedEmpresaId);


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
            Console.WriteLine(FechaInicio);
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
                            Text = reader["granja"].ToString().ToUpper(),
                    });
                    }
                }
            }
        }

        private void CargarPesadas(int empresaId, int granjaId, DateTime fechaInicio, DateTime fechaFin)
        {
            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();

                string sqlQueryGranjas = $"SELECT T0.idPesada, T0.Granja, T0.Lote, T0.Etapa_Finalizo, T0.Edad_Dias, T0.Edad_Semanas, T0.Cantidad_CerdosPesados, T0.PesadaTotal, (T0.PesadaTotal / T0.Cantidad_CerdosPesados) AS Peso_Promedio, ( SELECT T1.PesoFinal FROM TablaConsumos T1 WHERE T0.Edad_Dias >= T1.EdadEnDias_InicioConsumo AND T0.Edad_Dias <= T1.EdadEnDias_TerminoConsumo AND T1.TipoDeCerdos = ( CASE  WHEN T0.Sitio = '0' AND T0.Edad_Semanas >= '17' THEN 'HR' ELSE 'LP' END ) ) AS Peso_Meta, ( (T0.PesadaTotal / T0.Cantidad_CerdosPesados) - ( SELECT T1.PesoFinal FROM TablaConsumos T1 WHERE T0.Edad_Dias >= T1.EdadEnDias_InicioConsumo AND T0.Edad_Dias <= T1.EdadEnDias_TerminoConsumo AND T1.TipoDeCerdos = 'LP') ) AS Diferencia_Peso, T0.FechaHora_Pesada FROM [Pesadas_Reales_FinEtapa] T0 WHERE T0.idEmpresa = {empresaId}  value AND T0.Granja = {granjaId} AND T0.FechaHora_Pesada BETWEEN {fechaInicio} AND {fechaFin};";

                using (var command = new SqlCommand(sqlQueryGranjas, connection))
                using (var reader = command.ExecuteReader())
                {
                 Pesadas = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = " - - - - Selecciona una  - - - - " }
                };

                    while (reader.Read())
                    {
                       Pesadas.Add(new SelectListItem
                        {
                            Value = reader["idEmpresa"].ToString(),
                            Text = reader["granja"].ToString().ToUpper(),
                        });
                    }
                }



            }
        }





    }
}
