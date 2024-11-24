
namespace MDW_Back_ops.Models
{
    public class Bitacora
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Email { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
    }
}
