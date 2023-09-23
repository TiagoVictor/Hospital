using Application.MedicalRecord.Dto;

namespace Application.MedicalRecord.Responses
{
    public class MedicalResponse : Response
    {
        public MedicalRecordDto Data;
        public List<MedicalRecordDto> MedicalRecords = new();
    }
}
