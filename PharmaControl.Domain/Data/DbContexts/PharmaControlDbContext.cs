using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.Configurations;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.DbContexts
{
    public class PharmaControlDbContext
        : DbContext
    {
        public PharmaControlDbContext(
          DbContextOptions<PharmaControlDbContext> options)
          : base(options)
        {
        }

        public PharmaControlDbContext()
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineType> MedicineTypes { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<PharmacyStock> PharmacyStocks { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=AFTERNITY;Initial Catalog=PharmaControlDB;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new MedicineConfiguration());
            modelBuilder.ApplyConfiguration(new MedicineTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyStockConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
        }
    }
}
