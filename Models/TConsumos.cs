using DocumentFormat.OpenXml.Math;

namespace Reportes.Models
{
    public class TConsumos
    {

        //public int idPesada { get; set; }
        //public string? Granja { get; set; }
        //public int Lote { get; set; }
        //public string? Etapa_Finalizo { get; set; }
        //public int Edad_Dias { get; set; }
        //public int Edad_Semanas { get; set; }
        //public int Cantidad_CerdosPesados { get; set; }
        //public int PesadaTotal { get; set; }

        public int idEmpresa { get; set; }
        public string? Granja { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
