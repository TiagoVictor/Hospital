namespace Domain.Doctor.Exceptions
{
    public class CrmNullException : Exception
    {
        public override string Message => "Crm cannot be null";
    }
}
