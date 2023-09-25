using Application.Doctor.Dto;
using Application.Patient.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.Models
{
    public enum UserType
    {
        Doutor = 1,
        Paciente = 2,
    }
    public class LoginViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [DisplayName("Sobrenome")]
        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        public string LastName { get; set; }

        [DisplayName("Tipo de Usuário")]
        public UserType UserType { get; set; }

        [DisplayName("Nº de Celular")]
        [RegularExpression("^(?:(?:\\+|00)?(55)\\s?)?(?:\\(?([1-9][0-9])\\)?\\s?)?(?:((?:9\\d|[2-9])\\d{3})\\-?(\\d{4}))$", ErrorMessage = "Insira um numero de celular válido.")]
        [Required(ErrorMessage = "O campo Nº de Celular é obrigatório.")]
        public string CellPhoneNumber { get; set; }

        [DisplayName("Crm")]
        [Required(ErrorMessage = "O campo Crm é obrigatório.")]
        public string Crm { get; set; }

        public static DoctorDto ViewToDoctorDto(LoginViewModel loginViewModel)
        {
            return new DoctorDto
            {
                Name = loginViewModel.Name,
                LastName = loginViewModel.LastName,
                Crm = loginViewModel.Crm,
            };
        }

        public static PatientDto ViewToPatientDto(LoginViewModel loginViewModel)
        {
            return new PatientDto
            {
                Name = loginViewModel.Name,
                LastName = loginViewModel.LastName,
                CellPhoneNumber = loginViewModel.CellPhoneNumber,
            };
        }
    }
}
