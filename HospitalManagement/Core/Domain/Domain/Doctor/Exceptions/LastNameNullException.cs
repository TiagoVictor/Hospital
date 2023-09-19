namespace Domain.Doctor.Exceptions
{
    public class LastNameNullException : Exception
    {
        public override string Message => "Last name cannot be null";
    }
}
