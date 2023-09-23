using Application.Doctor.Dto;
using Application.Patient.Dto;
using System.ComponentModel;

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
        public string Name { get; set; }

        [DisplayName("Sobrenome")]
        public string LastName { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Tipo de Usuário")]
        public UserType UserType { get; set; }

        [DisplayName("Nº de Celular")]
        public string CellPhoneNumber { get; set; }

        [DisplayName("Crm")]
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
