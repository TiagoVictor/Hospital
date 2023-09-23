using Azure.Core;
using Domain.Patient.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Patient
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePatientAsync(Domain.Patient.Entities.Patient patient)
        {
            await _context
                .Patients
                .AddAsync(patient);

            await _context
                .SaveChangesAsync();

            return patient.Id;
        }

        public async Task DeletePatientAsync(Domain.Patient.Entities.Patient patient)
        {
            _context
                .Patients
                .Remove(patient);

            await _context
                .SaveChangesAsync();
        }

        public async Task<Domain.Patient.Entities.Patient> GetPatientByCellPhoneAsync(string cellPhone)
        {
            return await _context
                .Patients
                .FirstOrDefaultAsync(x => x.CellPhoneNumber == cellPhone);
        }

        public async Task<Domain.Patient.Entities.Patient> GetPatientByIdAsync(int id)
        {
            return await _context
                .Patients
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Domain.Patient.Entities.Patient>> GetPatientsAsync()
        {
            return await _context
                .Patients
                .ToListAsync();
        }

        public async Task<Domain.Patient.Entities.Patient> UpdatePatientAsync(Domain.Patient.Entities.Patient patient)
        {
            _context
                .Patients
                .Update(patient);

            await _context
                .SaveChangesAsync();

            return patient;
        }
    }
}
