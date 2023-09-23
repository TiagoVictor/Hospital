using Entity = Domain.Patient.Entities;

namespace Application.Patient.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CellPhoneNumber { get; set; }
        public static Entity.Patient MapToEntity(PatientDto dto)
        {
            return new Entity.Patient
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                CellPhoneNumber = dto.CellPhoneNumber,
            };
        }
        public static PatientDto MapToDto(Entity.Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                LastName = patient.LastName,
                CellPhoneNumber = patient.CellPhoneNumber,
            };
        }
    }
}
