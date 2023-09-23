using Application.Doctor.Dto;
using Application.Doctor.Ports;
using Application.Doctor.Requests;
using Application.Doctor.Responses;
using Domain.Doctor.Exceptions;
using Domain.Doctor.Ports;

namespace Application.Doctor
{
    public class DoctorManager : IDoctorManager
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorManager(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorResponse> CreateDoctorAsync(CreateDoctorRequest request)
        {
            try
            {
                var doctor = DoctorDto.MapToEntity(request.Data);
                await doctor.Save(_doctorRepository);
                request.Data.Id = doctor.Id;

                return new DoctorResponse
                {
                    Success = true,
                    Data = request.Data
                };
            }
            catch (NameNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_NAME
                };
            }
            catch (LastNameNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_LAST_NAME
                };
            }
            catch (CrmNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_CRM
                };
            }
            catch (Exception)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = "There was an error during using DB.",
                    ErrorCode = ErrorCodes.DOCTOR_COULD_NOT_SAVE
                };
            }
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);

            if (doctor == null)
                return;

            await _doctorRepository.DeleteDoctorAsync(doctor);
        }

        public async Task<DoctorResponse> GetDoctorAsync()
        {
            var doctors = await _doctorRepository.GetDoctorsAsync();
            var doctorResponse = new DoctorResponse();

            doctors.ForEach(x => doctorResponse.Doctors.Add(DoctorDto.MapToDto(x)));

            return new DoctorResponse
            {
                Doctors = doctorResponse.Doctors,
            };
        }

        public async Task<DoctorResponse> GetDoctorByCrmAsync(string crm)
        {
            var doctor = DoctorDto.MapToDto(await _doctorRepository.GetDoctorByCrmAsync(crm));

            if (doctor == null)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = "Doctor was not found",
                    ErrorCode = ErrorCodes.DOCTOR_NOT_FOUND
                };
            }

            return new DoctorResponse
            {
                Success = true,
                Data = doctor
            };
        }

        public async Task<DoctorResponse> GetDoctorByIdAsync(int id)
        {
            var doctor = DoctorDto.MapToDto(await _doctorRepository.GetDoctorById(id));

            if (doctor == null)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = "Doctor was not found",
                    ErrorCode = ErrorCodes.DOCTOR_NOT_FOUND
                };
            }

            return new DoctorResponse
            {
                Success = true,
                Data = doctor
            };
        }

        public async Task<DoctorResponse> UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorById(request.Id);

                if (doctor == null)
                {
                    return new DoctorResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.DOCTOR_NOT_FOUND,
                        Message = "Doctor was not found"
                    };
                }

                doctor.Name = request.Data.Name;
                doctor.LastName = request.Data.LastName;
                doctor.Crm = request.Data.Crm;

                await doctor.Save(_doctorRepository);

                request.Data.Id = doctor.Id;

                return new DoctorResponse
                {
                    Success = true,
                    Data = request.Data
                };

            }
            catch (NameNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_NAME
                };
            }
            catch (LastNameNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_LAST_NAME
                };
            }
            catch (CrmNullException ex)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.DOCTOR_INVALID_CRM
                };
            }
            catch (Exception)
            {
                return new DoctorResponse
                {
                    Success = false,
                    Message = "There was an error during using DB.",
                    ErrorCode = ErrorCodes.DOCTOR_COULD_NOT_UPDATE
                };
            }
        }
    }
}
