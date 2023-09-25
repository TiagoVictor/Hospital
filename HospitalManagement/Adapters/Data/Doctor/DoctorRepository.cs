using Domain.Doctor.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Doctor
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateDoctorAsync(Domain.Doctor.Entities.Doctor doctor)
        {
            await _context
                .Doctors
                .AddAsync(doctor);

            await _context
                .SaveChangesAsync();

            return doctor.Id;
        }

        public async Task DeleteDoctorAsync(Domain.Doctor.Entities.Doctor doctor)
        {
            _context
                .Doctors
                .Remove(doctor);

            await _context
                .SaveChangesAsync();
        }

        public async Task<Domain.Doctor.Entities.Doctor> GetDoctorByCrmAsync(string crm)
        {
            return await _context
                .Doctors
                .FirstOrDefaultAsync(x => x.Crm == crm);
        }

        public async Task<Domain.Doctor.Entities.Doctor> GetDoctorByIdAsync(int id)
        {
            return await _context
                .Doctors
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Domain.Doctor.Entities.Doctor>> GetDoctorsAsync()
        {
            return await _context
                .Doctors
                .ToListAsync();
        }

        public async Task<Domain.Doctor.Entities.Doctor> UpdateDoctorAsync(Domain.Doctor.Entities.Doctor doctor)
        {
            _context
               .Doctors
               .Update(doctor);

            await _context
                .SaveChangesAsync();

            return doctor;
        }
    }
}
