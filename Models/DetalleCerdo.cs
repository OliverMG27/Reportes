namespace Reportes.Models
{
    public class DetalleCerdo
    {
        public int idEmpresa { get; set; }
        public string? razonSocial { get; set; }
        public string? granja { get; set; }
        public string? fechaMovimiento { get; set; }
        public string? lote {  get; set; }
        public string? nave {  get; set; }
        public int cantidad { get; set; }

    }
}
