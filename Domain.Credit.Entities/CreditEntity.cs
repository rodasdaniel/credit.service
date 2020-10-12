using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Credit.Entities
{
    public class CreditEntity
    {
        [Key]
        public long IdCredito { get; set; }
        public long IdCliente { get; set; }
        public int Plazo { get; set; }
        public int Frecuencia { get; set; }
        public decimal ValorCapital { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
