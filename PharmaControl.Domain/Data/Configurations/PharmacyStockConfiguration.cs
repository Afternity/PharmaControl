using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class PharmacyStockConfiguration
        : IEntityTypeConfiguration<PharmacyStock>
    {
        public void Configure(EntityTypeBuilder<PharmacyStock> builder)
        {
            builder.HasKey(ps =>
                new
                {
                    ps.PharmacyId,
                    ps.MedicineId
                });
        }
    }
}
