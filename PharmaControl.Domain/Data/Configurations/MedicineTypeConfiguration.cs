using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class MedicineTypeConfiguration
        : IEntityTypeConfiguration<MedicineType>
    {
        public void Configure(EntityTypeBuilder<MedicineType> builder)
        {
            builder.HasKey(medicineType => medicineType.Id);

            builder.HasMany(medicineType => medicineType.Medicines)
                .WithOne(medicine => medicine.MedicineType)
                .HasForeignKey(medicine => medicine.MedicineTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
