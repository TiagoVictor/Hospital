using Domain.Doctor.Enum;
using Entity = Domain.Doctor.Entities;

namespace Application.Doctor.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Crm { get; set; }
        public Status Status { get; set; }

        public static Entity.Doctor MapToEntity(DoctorDto dto)
        {
            return new Entity.Doctor
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Crm = dto.Crm,
                Status = dto.Status,
            };
        }

        public static DoctorDto MapToDto(Entity.Doctor doctor)
        {
            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                LastName = doctor.LastName,
                Crm = doctor.Crm,
                Status = doctor.Status,
            };
        }
    }
}
