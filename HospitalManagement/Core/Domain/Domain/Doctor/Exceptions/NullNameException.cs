namespace Domain.Doctor.Exceptions
{
    public class NullNameException : Exception
    {
        public override string Message => "Name cannot be null";
    }
}
