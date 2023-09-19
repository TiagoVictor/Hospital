namespace Domain.Patient.Ports
{
    public interface IPatientRepository
    {
        Task<int> CreatePatientAsync(Entities.Patient patient);
        Task<Entities.Patient> UpdatePatientAsync(Entities.Patient patient);
        Task DeletePatientAsync(int id);
        Task<Entities.Patient> GetPatientByIdAsync(int id);
        Task<List<Entities.Patient>> GetPatientsAsync();
    }
}
