using Domain.Patient.Enum;
using Domain.Patient.Exceptions;
using Domain.Patient.Ports;
using Entity =  Domain.MedicalRecord.Entities;

namespace Domain.Patient.Entities
{
    public class Patient
    {
        public Patient()
        {
            Status = Status.Ativo;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CellPhoneNumber { get; set; }
        public Status Status { get; set; }
        public IList<Entity.MedicalRecord> MedicalRecords { get; set; }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name))
                throw new NameNullException();

            if (string.IsNullOrEmpty(LastName))
                throw new LastNameNullException();

            if (string.IsNullOrEmpty(CellPhoneNumber))
                throw new CellPhoneNullException();
        }

        public async Task Save(IPatientRepository repository)
        {
            ValidateState();

            if (Id == 0)
                Id = await repository.CreatePatientAsync(this);
            else
                await repository.UpdatePatientAsync(this);
        }
    }
}
