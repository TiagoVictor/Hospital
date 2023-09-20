using Data.Doctor.Mapping;
using Data.MedicalRecord.Mapping;
using Data.Patient.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<Domain.Doctor.Entities.Doctor> Doctors { get; set; }
        public DbSet<Domain.Patient.Entities.Patient> Patients { get; set; }
        public DbSet<Domain.MedicalRecord.Entities.MedicalRecord> MedicalRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorMap());
            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new MedicalRecordMap());
        }
    }
}
