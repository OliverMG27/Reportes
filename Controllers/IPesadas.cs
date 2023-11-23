using DocumentFormat.OpenXml.Math;
using Reportes.Models;
using System.Data.SqlClient;

namespace Reportes.Controllers
{
    public interface IPesadas
    {
        IEnumerable<Empresa> GetEmpresas();
        IEnumerable<Granja> GetGranjas(int empresaId);
        IEnumerable<TConsumos> GetReporte(int idEmpresa, string Granja, DateTime FechaInicio, DateTime FechaFin);
    }

    public class RPesadas : IPesadas
    {
        private readonly string _connectionString;

        public RPesadas(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IEnumerable<Empresa> GetEmpresas()
        {
            var empresas = new List<Empresa>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string queryEmpresa = "SELECT * FROM Empresas ORDER BY razonSocial";

                using (SqlCommand command = new SqlCommand(queryEmpresa, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        empresas.Add(new Empresa
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            razonSocial = reader.GetString(reader.GetOrdinal("razonSocial"))
                        });
                    }
                }
            }
            return empresas;
        }

        public IEnumerable<Granja> GetGranjas(int empresaId)
        {
            var granjas = new List<Granja>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string queryGranja = "SELECT idEmpresa, granja FROM Granjas WHERE idEmpresa = @idEmpresa AND ACTIVA = 'S' ORDER BY granja";

                using (SqlCommand command = new SqlCommand(queryGranja, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        granjas.Add(new Granja
                        {
                            idEmpresa = reader.GetInt32(reader.GetOrdinal("idEmpresa")),
                            granja = reader.GetString(reader.GetOrdinal("granja"))
                        });
                    }
                }
            }
            return granjas;
        }



        public IEnumerable<TConsumos> GetReporte(int idEmpresa, string Granja, DateTime FechaInicio, DateTime FechaFin)
        {
            var reporte = new List<TConsumos>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string queryGranja =" SELECT T0.idPesada, T0.Granja, T0.Lote, T0.Etapa_Finalizo, T0.Edad_Dias, T0.Edad_Semanas, T0.Cantidad_CerdosPesados, T0.PesadaTotal, (T0.PesadaTotal / T0.Cantidad_CerdosPesados) AS Peso_Promedio, (SELECT T1.PesoFinal FROM TablaConsumos T1 WHERE T0.Edad_Dias >= T1.EdadEnDias_InicioConsumo AND T0.Edad_Dias <= T1.EdadEnDias_TerminoConsumo AND T1.TipoDeCerdos = (CASE WHEN T0.Sitio = '0' AND T0.Edad_Semanas >= '17' THEN 'HR' ELSE 'LP'END)) AS Peso_Meta, ((T0.PesadaTotal / T0.Cantidad_CerdosPesados) - (SELECT T1.PesoFinal FROM TablaConsumos T1 WHERE T0.Edad_Dias >= T1.EdadEnDias_InicioConsumo AND T0.Edad_Dias <= T1.EdadEnDias_TerminoConsumo AND T1.TipoDeCerdos = 'LP')) AS Diferencia_Peso, T0.FechaHora_Pesada FROM[Pesadas_Reales_FinEtapa] T0 WHERE T0.idEmpresa = '@idEmpresa' AND T0.Granja = '@Granja' AND T0.FechaHora_Pesada BETWEEN '@FechaInicio' AND '@FechaFin'";

                using (SqlCommand command = new SqlCommand(queryGranja, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        reporte.Add(new TConsumos
                        {
                            idEmpresa = reader.GetInt32(reader.GetOrdinal("idEmpresa")),
                            Granja = reader.GetString(reader.GetOrdinal("Granja")),
                            FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                            FechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin"))
                        });
                    }
                }
            }
            return reporte;
        }







    }
}






















