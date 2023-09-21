﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    partial class HospitalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Doctor.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Crm");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<int>("Status")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("Domain.MedicalRecord.Entities.MedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex(new[] { "Id" }, "IDX_MedicalRecords_Id");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("Domain.Patient.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CellPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("CellPhoneNumber");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.Property<int>("Status")
                        .HasMaxLength(10)
                        .HasColumnType("INTEGER")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("Domain.MedicalRecord.Entities.MedicalRecord", b =>
                {
                    b.HasOne("Domain.Doctor.Entities.Doctor", "Doctor")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MedicalRecords_Doctor");

                    b.HasOne("Domain.Patient.Entities.Patient", "Patient")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MedicalRecords_Patient");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Domain.Doctor.Entities.Doctor", b =>
                {
                    b.Navigation("MedicalRecords");
                });

            modelBuilder.Entity("Domain.Patient.Entities.Patient", b =>
                {
                    b.Navigation("MedicalRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
