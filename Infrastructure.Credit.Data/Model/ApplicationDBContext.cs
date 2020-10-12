using Domain.Credit.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Credit.Data.Model
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<CreditEntity> Credito { get; set; }
        public DbSet<QuotaEntity> Cuota { get; set; }
        public DbSet<ClientEntity> Cliente { get; set; }
    }
}
