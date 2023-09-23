using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Domain.Doctor.Entities;

namespace Data.Doctor.Mapping
{
    public class DoctorMap : IEntityTypeConfiguration<Entity.Doctor>
    {
        public void Configure(EntityTypeBuilder<Entity.Doctor> builder)
        {
            builder.ToTable("Doctor");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Crm)
                .IsRequired()
                .HasColumnName("Crm")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.HasIndex(x => x.Id, "IDX_DOCTOR_ID");
            builder.HasIndex(x => x.Crm, "IDX_DOCTOR_CRM").IsUnique();
        }
    }
}
