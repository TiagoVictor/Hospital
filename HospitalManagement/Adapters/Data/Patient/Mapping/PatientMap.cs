using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Domain.Patient.Entities;

namespace Data.Patient.Mapping
{
    public class PatientMap : IEntityTypeConfiguration<Entity.Patient>
    {
        public void Configure(EntityTypeBuilder<Entity.Patient> builder)
        {
            builder.ToTable("Patient");

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

            builder.Property(x => x.CellPhoneNumber)
                .IsRequired()
                .HasColumnName("CellPhoneNumber")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("INTEGER")
                .HasMaxLength(10);

            builder.HasIndex(x => x.Id);
        }
    }
}
