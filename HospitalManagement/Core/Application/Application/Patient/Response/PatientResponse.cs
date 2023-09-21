using Application.Patient.Dto;

namespace Application.Patient.Responses
{
    public class PatientResponse : Response
    {
        public PatientDto Data;
        public List<PatientDto> Patients;
    }
}
