﻿namespace Application
{
    public enum ErrorCodes
    {
        // Doctor ErrorCode 1 to 99
        DOCTOR_INVALID_NAME = 1,
        DOCTOR_INVALID_LAST_NAME = 2,
        DOCTOR_INVALID_CRM = 3,
        DOCTOR_COULD_NOT_SAVE = 4,
        DOCTOR_COULD_NOT_UPDATE = 5,
        DOCTOR_NOT_FOUND = 6,

        // Patient ErrorCode 100 tp 199
        PATIENT_INVALID_NAME = 100,
        PATIENT_INVALID_LASTNAME = 101,
        PATIENT_INVALID_CELLPHONE_NUMBER = 102,
        PATIENT_COULD_NOT_SAVE = 103,
        PATIENT_COULD_NOT_UPDATE = 104,
        PATIENT_NOT_FOUND = 105,

        // MedicalRecord ErrorCode 200 to 299
        MEDICAL_RECORD_INVALID_PATIENT = 200,
        MEDICAL_RECORD_INVALID_DOCTOR = 201,
        MEDICAL_RECORD_COULD_NOT_SAVE = 202,
        MEDICAL_RECORD_COULD_NOT_UPDATE = 203,
        MEDICAL_RECORD_NOT_FOUND = 204,
        MEDICAL_RECORD_INVALID_DESCRIPTION = 205,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
