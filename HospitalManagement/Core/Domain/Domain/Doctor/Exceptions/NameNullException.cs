namespace Domain.Doctor.Exceptions
{
    public class NameNullException : Exception
    {
        public override string Message => "Name cannot be null";
    }
}
