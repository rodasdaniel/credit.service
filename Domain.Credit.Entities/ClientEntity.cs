using System.ComponentModel.DataAnnotations;

namespace Domain.Credit.Entities
{
    public class ClientEntity
    {
        [Key]
        public long IdCliente { get; set; }
        public decimal CupoDisponible { get; set; }
    }
}
