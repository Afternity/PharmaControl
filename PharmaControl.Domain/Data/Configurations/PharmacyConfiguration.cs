using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class PharmacyConfiguration
        : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasKey(pharmacy => pharmacy.Id);

            builder.HasMany(pharmacy => pharmacy.Employees)
                .WithOne(employee => employee.Pharmacy)
                .HasForeignKey(employee => employee.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(pharmacy => pharmacy.PharmacyStocks)
                .WithOne(pharmacyStacks => pharmacyStacks.Pharmacy)
                .HasForeignKey(pharmacyStacks => pharmacyStacks.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
