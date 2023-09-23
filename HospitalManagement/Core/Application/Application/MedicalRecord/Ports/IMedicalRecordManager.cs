using Application.MedicalRecord.Requests;
using Application.MedicalRecord.Responses;

namespace Application.MedicalRecord.Ports
{
    public interface IMedicalRecordManager
    {
        Task<MedicalResponse> CreateMedicalRecordAsync(CreateMedicalRecordRequest request);
        Task<MedicalResponse> UpdateMedicalRecordAsync(UpdateMedicalRecordRequest request);
        Task DeleteMedicalRecordAsync(int id);
        Task<MedicalResponse> GetMedicalRecordByIdAsync(int id);
        Task<MedicalResponse> GetMedicalRecordsAsync();
        Task<MedicalResponse> GetMedicalRecordsByPatientIdAsync(int patientId);
    }
}
