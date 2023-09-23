namespace Domain.MedicalRecord.Exceptions
{
    public class DescriptionNullException : Exception
    {
        public override string Message => "Description cannot be null.";
    }
}
