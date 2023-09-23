using Domain.MedicalRecord.Exceptions;
using Domain.MedicalRecord.Ports;

namespace Domain.MedicalRecord.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public Doctor.Entities.Doctor Doctor { get; set; } = new();
        public Patient.Entities.Patient Patient { get; set; } = new();
        public string Description { get; set; }

        public void ValidateState()
        {
            if (Patient == null)
                throw new PatientNullException();

            if (Doctor == null)
                throw new DoctorNullException();
            
            if (string.IsNullOrEmpty(Description))
                throw new DescriptionNullException();
        }

        public async Task Save(IMedicalRecordRepository repository)
        {
            ValidateState();

            if (Id == 0)
                Id = await repository.CreateMedicalRecordAsync(this);
            else
                await repository.UpdateMedicalRecordAsync(this);
        }
    }
}
