using Microservice.Crm.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Crm.DataLayer
{
    public class CrmDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public CrmDbContext(DbContextOptions<CrmDbContext> contextOptions)
            : base(contextOptions)
        {
        }
    }
}
