namespace Domain.MedicalRecord.Ports
{
    public interface IMedicalRecordRepository
    {
        Task<int> CreateMedicalRecordAsync(Entities.MedicalRecord medicalRecord);
        Task<Entities.MedicalRecord> UpdateMedicalRecordAsync(Entities.MedicalRecord medicalRecord);
        Task DeleteMedicalRecordAsync(int id);
        Task<Entities.MedicalRecord> GetMedicalRecordByIdAsync(int id);
        Task<List<Entities.MedicalRecord>> GetMedicalRecordsAsync();
    }
}
