using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Reportes.Models;
using System.Text;
using ClosedXML.Excel;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace Reportes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public FileResult exportar(string granja, string fechainicio, string fechafin)
        {

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;"))
            {
                StringBuilder consulta = new StringBuilder();
                consulta.AppendLine("SET DATEFORMAT dmy;");
                consulta.AppendLine("select * from [dbo].[Granjas] where idEmpresa = iif(@empresa =0,employeeI,@employee) and convert(date,OrderDate) between @fechainicio and @fechafin");


                SqlCommand cmd = new SqlCommand(consulta.ToString(), cn);
                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            dt.TableName = "Datos";

            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(dt);

                hoja.ColumnsUsed().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    libro.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte " + DateTime.Now.ToString() + ".xlsx");
                }
            }


        }


        public JsonResult obtenerEmpleado()
        {
            List<Reporte> listaEmpleado = new List<Reporte>();

            SqlConnection cn = new SqlConnection("Data Source=10.1.0.11;TrustServerCertificate=true; Initial Catalog=Pruebas_chCerdos_Rodrigo19_00hrs;Trusted_Connection=false; multisubnetfailover=true; User ID=sa;Password=B1Admin;");
            {
                SqlCommand cmd = new SqlCommand("select * from Granjas", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaEmpleado.Add(new Reporte()
                        {
                            idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                            granja = dr["Nombre"].ToString()
                        });
                    }
                }
            }

            return Json(listaEmpleado, JsonRequestBehavior.AllowGet);
        }


    }
}