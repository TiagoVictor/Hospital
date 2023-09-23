using Application.Doctor.Requests;
using Application.Doctor.Responses;

namespace Application.Doctor.Ports
{
    public interface IDoctorManager
    {
        Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request);
        Task<DoctorResponse> UpdateDoctorAsync(UpdateDoctorRequest request);
        Task DeleteDoctorAsync(int id);
        Task<DoctorResponse> GetDoctorByIdAsync(int id);
        Task<DoctorResponse> GetDoctorAsync();
        Task<DoctorResponse> GetDoctorByCrmAsync(string crm);
    }
}
