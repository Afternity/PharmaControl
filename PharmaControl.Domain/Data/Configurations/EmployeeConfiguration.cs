using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaControl.Domain.Models;

namespace PharmaControl.Domain.Data.Configurations
{
    public class EmployeeConfiguration
        : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(employee => employee.Id);

            builder.HasIndex(employee => employee.Email)
                .IsUnique();

            builder.HasOne(employee => employee.Pharmacy)
                .WithMany(pharmacy => pharmacy.Employees)
                .HasForeignKey(employee => employee.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
