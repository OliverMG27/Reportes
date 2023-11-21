using Reportes.Models;
using System.Data.SqlClient;

namespace Reportes.Controllers
{
    public interface IRCGranjas
    {
        IEnumerable<Empresa> GetEmpresas();
        IEnumerable<Granja> GetGranjas(int empresaId);
    }

    public class RCGranjas : IPesadas
    {
        private readonly string _connectionString;

        public RCGranjas(string connectionString)
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

    }
}
