using Application.MedicalRecord.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalWeb.Models
{
    public class MedicalRecordViewModel
    {
        public int Id { get; set; }

        [DisplayName("Doutor")]
        public DoctorViewModel Doctor { get; set; } = new();

        [DisplayName("Paciente")]
        [Required(ErrorMessage = "O campo Paciente é obrigatório.")]
        public PatientViewModel Patient { get; set; } = new();
        public List<MedicalRecordViewModel> Models { get; set; } = new();
        public List<PatientViewModel> Patients { get; set; } = new();

        public static MedicalRecordDto ViewToDto(MedicalRecordViewModel viewModel)
        {
            return new MedicalRecordDto
            {
                Id = viewModel.Id,
                DoctorDto = DoctorViewModel.ViewToDto(viewModel.Doctor),
                PatientDto = PatientViewModel.ViewToDto(viewModel.Patient)
            };
        }

        public static MedicalRecordViewModel DtoToView(MedicalRecordDto medicalRecordDto)
        {
            return new MedicalRecordViewModel
            {
                Id = medicalRecordDto.Id,
                Doctor = DoctorViewModel.DtoToView(medicalRecordDto.DoctorDto),
                Patient = PatientViewModel.DtoToView(medicalRecordDto.PatientDto)
            };
        }
    }
}
