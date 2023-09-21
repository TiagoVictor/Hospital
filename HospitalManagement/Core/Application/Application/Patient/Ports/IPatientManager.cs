using Application.Patient.Requests;
using Application.Patient.Responses;

namespace Application.Patient.Ports
{
    public interface IPatientManager
    {
        Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request);
        Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request);
        Task DeletePatientAsync(int id);
        Task<PatientResponse> GetPatientByIdAsync(int id);
        Task<PatientResponse> GetPatientsAsync();
    }
}
