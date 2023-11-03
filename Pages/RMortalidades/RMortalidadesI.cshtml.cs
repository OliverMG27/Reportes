using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;


namespace Reportes.Pages.RMortalidades
{
    public class RMortalidadesIModel : PageModel
    {
             public List<Empresa> Empresas { get; set; } // Crear una propiedad para almacenar las empresas

        public void OnGet()
        {
            Empresas = new List<Empresa>();
            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";
            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();
                string sql = "SELECT * FROM Empresas ORDER BY razonSocial";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empresas.Add(new Empresa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                RazonSocial = reader.GetString(reader.GetOrdinal("razonSocial"))
                            });
                        }
                    }
                }
            }
        }
    }

    public class Empresa
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
    }
}