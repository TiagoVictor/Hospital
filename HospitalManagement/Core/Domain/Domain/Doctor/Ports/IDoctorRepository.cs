namespace Domain.Doctor.Ports
{
    public interface IDoctorRepository
    {
        Task<int> CreateDoctorAsync(Entities.Doctor doctor);
        Task<Entities.Doctor> UpdateDoctorAsync(Entities.Doctor doctor);
        Task DeleteDoctorAsync(int id);
        Task<Entities.Doctor> GetDoctorById(int id);
        Task<List<Entities.Doctor>> GetDoctorsAsync();
    }
}
