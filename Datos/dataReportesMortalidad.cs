using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using Reportes.Models;
using Reportes.Pages.RMortalidades;
using System.Data.SqlClient;


namespace Reportes.Datos
{
    public class dataReportesMortalidad
    {
        private readonly SqlConnection _connection;
        string connectionString = "Data Source=LAPTOP-2EE4AKBJ\\MSSQLSERVER00;Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Persist Security Info=True;User ID=sa;Password=3301701;";

        public dataReportesMortalidad(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("connectionString"));
        }

        public async Task<List<Empresa>> GetItemsAsync()
        {

            using (var connection = new SqlConnection(connectionString))
            {

                await _connection.OpenAsync();

                string sqlQuery = "SELECT * FROM Empresas ORDER BY razonSocial";

                using (var command = new SqlCommand(sqlQuery, _connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var items = new List<Empresa>();

                    while (await reader.ReadAsync())
                    {
                        var item = new Empresa
                        {
                            id = reader.GetInt32(0),
                            RazonSocial = reader.GetString(1)
                        };
                        items.Add(item);
                    }

                    return items;
                }
            }
        }

        public async Task<List<Granja>> GetSecondItemsAsync(int SelectValue)
        { 
            await _connection.OpenAsync();

            using (var command = new SqlCommand($"SELECT granja FROM Granjas WHERE idEmpresa = {SelectValue} AND Activa = 'S' ORDER BY granja", _connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                var items = new List<Granja>();

                while (await reader.ReadAsync())
                {
                    var item = new Granja
                    {
                        idEmpresa = reader.GetInt32(0),
                        granja = reader.GetString(1)
                    };
                    items.Add(item);
                }

                return items;
            }
        }
    }
}
