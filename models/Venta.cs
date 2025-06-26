using System.ComponentModel.DataAnnotations;

namespace ComisionApi.Models
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaVenta { get; set; }
        public string Vendedor { get; set; } = null!;
        public decimal Monto { get; set; }
    }
}