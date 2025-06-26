using System.ComponentModel.DataAnnotations;

namespace ComisionApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
    }
}