namespace Domain.Patient.Exceptions
{
    public class LastNameNullException : Exception
    {
        public override string Message => "Last Name cannot be null";
    }
}
