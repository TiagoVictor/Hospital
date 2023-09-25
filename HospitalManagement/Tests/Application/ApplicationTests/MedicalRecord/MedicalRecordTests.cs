using Application.Doctor.Dto;
using Application.Doctor.Requests;
using Application.Doctor;
using Application;
using Application.MedicalRecord;
using Domain.Doctor.Entities;
using Domain.Doctor.Ports;
using Moq;
using Domain.MedicalRecord.Ports;
using Domain.MedicalRecord.Entities;
using Application.MedicalRecord.Dto;
using Application.Patient.Dto;
using Application.MedicalRecord.Requests;
using Domain.Patient.Ports;
using Domain.Patient.Entities;
using System.Numerics;

namespace ApplicationTests
{
    public class MedicalRecordTests
    {
        MedicalRecordManager? medicalRecordManager;


        [Fact]
        public async Task HappyPath()
        {
            var patient = new Patient
            {
                Id = 1
            };

            var doctor = new Doctor
            {
                Id = 1
            };

            var medicalRecordDto = new MedicalRecordDto
            {
                Description = "Fulano",
                PatientDto = new PatientDto { Id = 1},
                DoctorDto = new DoctorDto { Id = 1}
            };

            int expectedId = 1;

            var request = new CreateMedicalRecordRequest
            {
                Data = medicalRecordDto,
            };

            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateMedicalRecordAsync(It.IsAny<MedicalRecord>())).Returns(Task.FromResult(expectedId));
            fakeRepoPat.Setup(x => x.GetPatientByIdAsync(1)).Returns(Task.FromResult(patient));
            fakeRepoDoc.Setup(x => x.GetDoctorByIdAsync(1)).Returns(Task.FromResult(doctor));
            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.CreateMedicalRecordAsync(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(expectedId, res.Data.Id);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_DescriptionNullException_WhenDescriptionIsNull(string description)
        {
            var patient = new Patient
            {
                Id = 1
            };

            var doctor = new Doctor
            {
                Id = 1
            };

            var medicalRecordDto = new MedicalRecordDto
            {
                Description = null,
                PatientDto = new PatientDto { Id = 1 },
                DoctorDto = new DoctorDto { Id = 1 }
            };

            var request = new CreateMedicalRecordRequest
            {
                Data = medicalRecordDto,
            };

            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateMedicalRecordAsync(It.IsAny<MedicalRecord>())).Returns(Task.FromResult(1));
            fakeRepoPat.Setup(x => x.GetPatientByIdAsync(1)).Returns(Task.FromResult(patient));
            fakeRepoDoc.Setup(x => x.GetDoctorByIdAsync(1)).Returns(Task.FromResult(doctor));
            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.CreateMedicalRecordAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MEDICAL_RECORD_INVALID_DESCRIPTION, res.ErrorCode);
            Assert.Equal("Description cannot be null.", res.Message);
        }

        [Fact]
        public async Task Should_Return_PatientNameNullException_WhenPatientIsNull()
        {
            var patient = new Patient
            {
                Id = 1
            };

            var doctor = new Doctor
            {
                Id = 1
            };

            var medicalRecordDto = new MedicalRecordDto
            {
                Description = "Fulano",
                PatientDto = new PatientDto (),
                DoctorDto = new DoctorDto { Id = 1 }
            };

            var request = new CreateMedicalRecordRequest
            {
                Data = medicalRecordDto,
            };

            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateMedicalRecordAsync(It.IsAny<MedicalRecord>())).Returns(Task.FromResult(1));
            fakeRepoPat.Setup(x => x.GetPatientByIdAsync(1)).Returns(Task.FromResult(patient));
            fakeRepoDoc.Setup(x => x.GetDoctorByIdAsync(1)).Returns(Task.FromResult(doctor));
            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.CreateMedicalRecordAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MEDICAL_RECORD_INVALID_PATIENT, res.ErrorCode);
            Assert.Equal("Patient cannot be null", res.Message);
        }

        [Fact]
        public async Task Should_Return_DoctorNullException_WhenDoctorIsNull()
        {
            var patient = new Patient
            {
                Id = 1
            };

            var doctor = new Doctor
            {
                Id = 1
            };

            var medicalRecordDto = new MedicalRecordDto
            {
                Description = "Fulano",
                PatientDto = new PatientDto { Id = 1 },
                DoctorDto = new DoctorDto ()
            };

            var request = new CreateMedicalRecordRequest
            {
                Data = medicalRecordDto,
            };

            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateMedicalRecordAsync(It.IsAny<MedicalRecord>())).Returns(Task.FromResult(1));
            fakeRepoPat.Setup(x => x.GetPatientByIdAsync(1)).Returns(Task.FromResult(patient));
            fakeRepoDoc.Setup(x => x.GetDoctorByIdAsync(1)).Returns(Task.FromResult(doctor));
            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.CreateMedicalRecordAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MEDICAL_RECORD_INVALID_DOCTOR, res.ErrorCode);
            Assert.Equal("Doctor cannot be null", res.Message);
        }

        [Fact]
        public async Task Should_Return_MedicalRecordNotFound_When_MedicalRecordDoesentExist()
        {
            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();

            var medicalRecord = new MedicalRecord();

            fakeRepo.Setup(x => x.GetMedicalRecordByIdAsync(333)).Returns(Task.FromResult<MedicalRecord>(medicalRecord));

            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.GetMedicalRecordByIdAsync(333);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MEDICAL_RECORD_NOT_FOUND, res.ErrorCode);
            Assert.Equal("Medical Record not found", res.Message);
        }

        [Fact]
        public async Task Should_Return_Doctor_Success()
        {
            var fakeRepo = new Mock<IMedicalRecordRepository>();
            var fakeRepoPat = new Mock<IPatientRepository>();
            var fakeRepoDoc = new Mock<IDoctorRepository>();

            var fakeMedical = new MedicalRecord
            {
                Id = 1,
                Description = "Test",
                Doctor = new Doctor(),
                Patient = new Domain.Patient.Entities.Patient()
            };

            fakeRepo.Setup(x => x.GetMedicalRecordByIdAsync(1)).Returns(Task.FromResult((MedicalRecord?)fakeMedical));

            medicalRecordManager = new MedicalRecordManager(fakeRepo.Object, fakeRepoPat.Object, fakeRepoDoc.Object);

            var res = await medicalRecordManager.GetMedicalRecordByIdAsync(1);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(fakeMedical.Id, res.Data.Id);
            Assert.Equal(fakeMedical.Description, res.Data.Description);
        }
    }
}
