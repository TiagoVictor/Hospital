namespace Domain.MedicalRecord.Exceptions
{
    public class PatientNullException : Exception
    {
        public override string Message => "Patient cannot be null";
    }
}
