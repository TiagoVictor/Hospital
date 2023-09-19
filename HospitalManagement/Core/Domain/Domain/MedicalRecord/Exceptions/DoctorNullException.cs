namespace Domain.MedicalRecord.Exceptions
{
    public class DoctorNullException : Exception
    {
        public override string Message => "Doctor cannot be null";
    }
}
