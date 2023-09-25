namespace Domain.Doctor.Ports
{
    public interface IDoctorRepository
    {
        Task<int> CreateDoctorAsync(Entities.Doctor doctor);
        Task<Entities.Doctor> UpdateDoctorAsync(Entities.Doctor doctor);
        Task DeleteDoctorAsync(Entities.Doctor doctor);
        Task<Entities.Doctor> GetDoctorByIdAsync(int id);
        Task<List<Entities.Doctor>> GetDoctorsAsync();
        Task<Entities.Doctor> GetDoctorByCrmAsync(string crm);
    }
}
