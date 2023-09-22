using Application.Doctor.Dto;

namespace Application.Doctor.Responses
{
    public class DoctorResponse : Response
    {
        public DoctorDto Data;
        public List<DoctorDto> Doctors = new();
    }
}
