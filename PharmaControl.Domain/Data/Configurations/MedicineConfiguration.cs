using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class MedicineConfiguration
        : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(medicine => medicine.Id);

            builder.HasOne(medicine => medicine.MedicineType)
                .WithMany(medicineType => medicineType.Medicines)
                .HasForeignKey(medicine => medicine.MedicineTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
