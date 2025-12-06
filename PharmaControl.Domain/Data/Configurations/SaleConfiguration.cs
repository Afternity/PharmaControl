using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class SaleConfiguration
        : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(sale => sale.Id);

            builder.HasOne(sale => sale.PharmacyStock)
                .WithMany(ps => ps.Sales)
                .HasForeignKey(sale => new { sale.PharmacyId, sale.MedicineId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
