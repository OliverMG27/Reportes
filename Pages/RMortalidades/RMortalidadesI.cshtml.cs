using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Ajax.Utilities;
using DocumentFormat.OpenXml.EMMA;
using System.Reflection.Metadata;

namespace Reportes.Pages.RMortalidades
{
    public class RMortalidadesIModel : PageModel
    {
        public List<Empresa> Empresas { get; set; }
        public List<Granja> Granjas { get; set; }
        public string SelectedValue { get; set; }





        public void OnGet()
        {
            Empresas = new List<Empresa>();
            Granjas = new List<Granja>();

            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";

            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM Empresas ORDER BY razonSocial";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empresas.Add(new Empresa
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                RazonSocial = reader.GetString(reader.GetOrdinal("razonSocial"))
                            });
                        }
                    }
                }


             

            }
        }

        public void OnPost()
        {
            Empresas = new List<Empresa>();
            Granjas = new List<Granja>();

            string connectionString = "Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;";

            System.Data.SqlClient.SqlConnection connection = new(connectionString);
            {
                connection.Open();

                string sqlQueryGranjas = "SELECT granja FROM Granjas WHERE idEmpresa = @idEmpresa AND Activa = 'S' ORDER BY granja";
                using (SqlCommand command = new SqlCommand(sqlQueryGranjas, connection))
                {
                    command.Parameters.AddWithValue("@idEmpresa", SelectedValue);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Granjas.Add(new Granja
                            {
                                // Assuming idEmpresa is an int, if it's a different type, adjust accordingly
                                idEmpresa = reader.GetInt32(reader.GetOrdinal("idEmpresa")),
                                granja = reader.GetString(reader.GetOrdinal("granja"))
                            });
                        }
                    }
                }
            }

        }

    }



    public class Empresa
    {
        public int id { get; set; }
        public string RazonSocial { get; set; }
    }

    public class Granja
    {
        public int idEmpresa { get; set; }
        public string granja { get; set; }
    }

}

/*comentario de prueba*/