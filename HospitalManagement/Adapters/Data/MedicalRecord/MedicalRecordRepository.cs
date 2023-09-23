using Domain.MedicalRecord.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.MedicalRecord
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HospitalDbContext _context;

        public MedicalRecordRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateMedicalRecordAsync(Domain.MedicalRecord.Entities.MedicalRecord medicalRecord)
        {
            await _context
                .MedicalRecords
                .AddAsync(medicalRecord);

            await _context
                .SaveChangesAsync();

            return medicalRecord.Id;
        }

        public async Task DeleteMedicalRecordAsync(Domain.MedicalRecord.Entities.MedicalRecord medicalRecord)
        {
            _context
                .MedicalRecords
                .Remove(medicalRecord);

            await _context
                .SaveChangesAsync();
        }

        public async Task<Domain.MedicalRecord.Entities.MedicalRecord> GetMedicalRecordByIdAsync(int id)
        {
            return await _context
                .MedicalRecords
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Domain.MedicalRecord.Entities.MedicalRecord>> GetMedicalRecordsAsync()
        {
            return await _context
                .MedicalRecords
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .ToListAsync();
        }

        public async Task<List<Domain.MedicalRecord.Entities.MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _context
                .MedicalRecords
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Where(x => x.Patient.Id == patientId)
                .ToListAsync();
        }

        public async Task<Domain.MedicalRecord.Entities.MedicalRecord> UpdateMedicalRecordAsync(Domain.MedicalRecord.Entities.MedicalRecord medicalRecord)
        {
            _context
                .MedicalRecords
                .Update(medicalRecord);

            await _context
                .SaveChangesAsync();

            return medicalRecord;
        }
    }
}
