using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Doctor.Dto;

namespace HospitalWeb.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [DisplayName("Sobrenome")]
        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        public string LastName { get; set; }

        [DisplayName("Crm")]
        [Required(ErrorMessage = "O campo Crm é obrigatório.")]
        public string Crm { get; set; }
        public List<DoctorViewModel> Models { get; set; } = new();

        public static DoctorDto ViewToDto(DoctorViewModel doctorViewModel)
        {
            return new DoctorDto
            {
                Id = doctorViewModel.Id,
                Name = doctorViewModel.Name,
                LastName = doctorViewModel.LastName,
                Crm = doctorViewModel.Crm,
            };
        }

        public static DoctorViewModel DtoToView(DoctorDto doctorDto)
        {
            return new DoctorViewModel
            {
                Id = doctorDto.Id,
                Name = doctorDto.Name,
                LastName = doctorDto.LastName,
                Crm = doctorDto.Crm,
            };
        }
    }
}
