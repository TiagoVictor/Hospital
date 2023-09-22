using Application.Doctor.Dto;
using Application.Patient.Dto;
using Entity = Domain.MedicalRecord.Entities;

namespace Application.MedicalRecord.Dto
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public DoctorDto DoctorDto { get; set; } = new();
        public PatientDto PatientDto { get; set; } = new();
        public static Entity.MedicalRecord MapToEntity(MedicalRecordDto dto)
        {
            return new Entity.MedicalRecord
            {
                Id = dto.Id,
                Doctor = DoctorDto.MapToEntity(dto.DoctorDto),
                Patient = PatientDto.MapToEntity(dto.PatientDto)
            };
        }
        public static MedicalRecordDto MapToDto(Entity.MedicalRecord medicalRecord)
        {
            return new MedicalRecordDto
            {
                Id = medicalRecord.Id,
                DoctorDto = DoctorDto.MapToDto(medicalRecord.Doctor),
                PatientDto = PatientDto.MapToDto(medicalRecord.Patient)
            };
        }
    }
}
