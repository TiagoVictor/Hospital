using Application.Doctor.Dto;
using Application.MedicalRecord.Dto;
using Application.MedicalRecord.Ports;
using Application.MedicalRecord.Requests;
using Application.MedicalRecord.Responses;
using Application.Patient.Dto;
using Domain.Doctor.Ports;
using Domain.MedicalRecord.Exceptions;
using Domain.MedicalRecord.Ports;
using Domain.Patient.Ports;

namespace Application.MedicalRecord
{
    public class MedicalRecordManager : IMedicalRecordManager
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public MedicalRecordManager(IMedicalRecordRepository medicalRecordRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<MedicalResponse> CreateMedicalRecordAsync(CreateMedicalRecordRequest request)
        {
            try
            {
                var medicalRecord = MedicalRecordDto.MapToEntity(request.Data);
                medicalRecord.Patient = await _patientRepository.GetPatientByIdAsync(request.Data.PatientDto.Id);
                medicalRecord.Doctor = await _doctorRepository.GetDoctorById(request.Data.DoctorDto.Id);

                await medicalRecord.Save(_medicalRecordRepository);
                request.Data.Id = request.Data.Id;

                return new MedicalResponse
                {
                    Success = true,
                    Data = request.Data,
                };
            }
            catch (DoctorNullException ex)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_INVALID_DOCTOR
                };
            }
            catch (PatientNullException ex)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_INVALID_PATIENT
                };
            }
            catch (Exception)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = "There was an error when using DB.",
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_COULD_NOT_SAVE
                };
            }
        }

        public async Task DeleteMedicalRecordAsync(int id)
        {
            var medicalRecord = await _medicalRecordRepository.GetMedicalRecordByIdAsync(id);

            if (medicalRecord == null)
                return;

            await _medicalRecordRepository.DeleteMedicalRecordAsync(medicalRecord);
        }

        public async Task<MedicalResponse> GetMedicalRecordByIdAsync(int id)
        {
            var medicalRecord = MedicalRecordDto.MapToDto(await _medicalRecordRepository.GetMedicalRecordByIdAsync(id));

            if (medicalRecord == null)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = "Medical Record not found",
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_NOT_FOUND
                };
            }

            return new MedicalResponse
            {
                Data = medicalRecord
            };
        }

        public async Task<MedicalResponse> GetMedicalRecordsAsync()
        {
            var medicalRecords = await _medicalRecordRepository.GetMedicalRecordsAsync();
            var medicalResponse = new MedicalResponse();

            medicalRecords.ForEach(x => medicalResponse.MedicalRecords.Add(MedicalRecordDto.MapToDto(x)));

            return new MedicalResponse
            {
                MedicalRecords = medicalResponse.MedicalRecords
            };
        }

        public async Task<MedicalResponse> UpdateMedicalRecordAsync(UpdateMedicalRecordRequest request)
        {
            try
            {
                var medicalRecord = await _medicalRecordRepository.GetMedicalRecordByIdAsync(request.Id);

                if (medicalRecord == null)
                {
                    return new MedicalResponse
                    {
                        Success = false,
                        Message = "Medical Record not found.",
                        ErrorCode = ErrorCodes.MEDICAL_RECORD_NOT_FOUND
                    };
                }

                medicalRecord.Doctor = DoctorDto.MapToEntity(request.Data.DoctorDto);
                medicalRecord.Patient = PatientDto.MapToEntity(request.Data.PatientDto);

                await medicalRecord.Save(_medicalRecordRepository);

                request.Data.Id = medicalRecord.Id;

                return new MedicalResponse
                {
                    Data = request.Data,
                };
                
            }
            catch (DoctorNullException ex)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_INVALID_DOCTOR
                };
            }
            catch (PatientNullException ex)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_INVALID_PATIENT
                };
            }
            catch (Exception)
            {
                return new MedicalResponse
                {
                    Success = false,
                    Message = "There was an error when using DB.",
                    ErrorCode = ErrorCodes.MEDICAL_RECORD_COULD_NOT_UPDATE
                };
            }
        }
    }
}
