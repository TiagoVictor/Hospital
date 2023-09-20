using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MedicalRecordEntity = Domain.MedicalRecord.Entities;

namespace Data.MedicalRecord.Mapping
{
    public class MedicalRecordMap : IEntityTypeConfiguration<MedicalRecordEntity.MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecordEntity.MedicalRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasIndex(x =>  x.Id, "IDX_MedicalRecords_Id");

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.MedicalRecords)
                .HasConstraintName("FK_MedicalRecords_Patient")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.MedicalRecords)
                .HasConstraintName("FK_MedicalRecords_Doctor")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
