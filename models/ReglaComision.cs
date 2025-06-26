using System.ComponentModel.DataAnnotations;

namespace ComisionApi.Models
{
    public class ReglaComision
    {
        [Key]
        public int ReglaId { get; set; }

        public decimal Rule { get; set; } 
        public decimal Amount { get; set; }
    }
}