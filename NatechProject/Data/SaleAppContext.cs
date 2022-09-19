using Microsoft.EntityFrameworkCore;
using NatechProject.Models;

namespace NatechProject.Data
{
    public class SaleAppContext : DbContext
    {
        public SaleAppContext(DbContextOptions<SaleAppContext> options) : base (options)
        { 

        }

        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<Sale> Sales { get; set; }


    }
}
