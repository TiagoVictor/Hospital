using Application.Patient.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage ="Campo Nome é obrigatório.")]
        public string Name { get; set; }

        [DisplayName("Sobrenome")]
        [Required(ErrorMessage = "Campo Sobrenome é obrigatório.")]
        public string LastName { get; set; }

        [DisplayName("Numero de Celular")]
        [Required(ErrorMessage = "Campo Numero de Celular é obrigatório.")]
        public string CellPhoneNumber { get; set; }
        public List<PatientViewModel> Models { get; set; } = new();
        public List<MedicalRecordViewModel> MedicalRecords { get; set; } = new();

        public static PatientDto ViewToDto (PatientViewModel model)
        {
            return new PatientDto
            {
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                CellPhoneNumber = model.CellPhoneNumber,
            };
        }

        public static PatientViewModel DtoToView (PatientDto dto)
        {
            return new PatientViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                CellPhoneNumber = dto.CellPhoneNumber,
            };
        }
    }
}
