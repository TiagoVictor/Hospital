namespace Domain.Patient.Exceptions
{
    public class CellPhoneNullException : Exception
    {
        public override string Message => "CellPhone cannot be null";
    }
}
