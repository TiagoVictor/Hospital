using Application.Patient.Dto;
using Application.Patient.Ports;
using Application.Patient.Requests;
using Application.Patient.Responses;
using Domain.Patient.Exceptions;
using Domain.Patient.Ports;

namespace Application.Patient
{
    public class PatientManager : IPatientManager
    {
        private readonly IPatientRepository _patientRepository;

        public PatientManager(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientResponse> CreatePatientAsync(CreatePatientRequest request)
        {
            try
            {
                var patient = PatientDto.MapToEntity(request.Data);
                await patient.Save(_patientRepository);
                request.Data.Id = patient.Id;

                return new PatientResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (NameNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_NAME
                };
            }
            catch (LastNameNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_LASTNAME
                };
            }
            catch (CellPhoneNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_CELLPHONE_NUMBER
                };
            }
            catch (Exception)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = "There was an error when using DB.",
                    ErrorCode = ErrorCodes.PATIENT_COULD_NOT_SAVE
                };
            }
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);

            if (patient == null)
                return;

            await _patientRepository.DeletePatientAsync(patient);
        }

        public async Task<PatientResponse> GetPatientByCellPhoneAsync(string cellPhone)
        {
            var patient = PatientDto.MapToDto(await _patientRepository.GetPatientByCellPhoneAsync(cellPhone));

            if (patient == null)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = "Patient not found.",
                    ErrorCode = ErrorCodes.PATIENT_NOT_FOUND
                };
            }

            return new PatientResponse
            {
                Success = true,
                Data = patient
            };
        }

        public async Task<PatientResponse> GetPatientByIdAsync(int id)
        {
            var patient = PatientDto.MapToDto(await _patientRepository.GetPatientByIdAsync(id));

            if (patient == null)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = "Patient not found.",
                    ErrorCode = ErrorCodes.PATIENT_NOT_FOUND
                };
            }

            return new PatientResponse
            {
                Success = true,
                Data = patient
            };
        }

        public async Task<PatientResponse> GetPatientsAsync()
        {
            var patients = await _patientRepository.GetPatientsAsync();
            var patientResponse = new PatientResponse();

            patients.ForEach(x => patientResponse.Patients.Add(PatientDto.MapToDto(x)));

            return patientResponse;
        }

        public async Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request)
        {
            try
            {
                var patient = await _patientRepository.GetPatientByIdAsync(request.Id);

                if (patient == null)
                {
                    return new PatientResponse
                    {
                        Success = false,
                        Message = "Patient not found.",
                        ErrorCode = ErrorCodes.PATIENT_NOT_FOUND
                    };
                }

                patient.Name = request.Data.Name;
                patient.LastName = request.Data.LastName;
                patient.CellPhoneNumber = request.Data.CellPhoneNumber;
                patient.Status = request.Data.Status;

                await patient.Save(_patientRepository);

                request.Data.Id = patient.Id;

                return new PatientResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (NameNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_NAME
                };
            }
            catch (LastNameNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_LASTNAME
                };
            }
            catch (CellPhoneNullException ex)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.PATIENT_INVALID_CELLPHONE_NUMBER
                };
            }
            catch (Exception)
            {
                return new PatientResponse
                {
                    Success = false,
                    Message = "There was an error when using DB.",
                    ErrorCode = ErrorCodes.PATIENT_COULD_NOT_UPDATE
                };
            }
        }
    }
}
