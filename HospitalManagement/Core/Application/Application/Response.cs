namespace Application
{
    public enum ErrorCodes
    {
        // Doctor ErrorCode 1 to 99
        DOCTOR_INVALID_NAME = 1,
        DOCTOR_INVALID_LAST_NAME = 2,
        DOCTOR_INVALID_CRM = 3,
        DOCTOR_COLD_NOT_SAVE = 4,
        DOCTOR_COLD_NOT_UPDATE = 5,
        DOCTOR_NOT_FOUND = 6,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
