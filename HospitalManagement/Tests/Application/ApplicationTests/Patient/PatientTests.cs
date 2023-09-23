using Application;
using Moq;
using Application.Patient;
using Domain.Patient.Ports;
using Domain.Patient.Entities;
using Application.Patient.Dto;
using Application.Patient.Requests;

namespace ApplicationTests
{
    public class PatientTests
    {
        PatientManager? patientManager;

        [Fact]
        public async Task HappyPath()
        {
            var patientDto = new PatientDto
            {
                Name = "Fulano",
                LastName = "Ciclano",
                CellPhoneNumber = "(16) 99380-2468"
            };

            int expectedId = 1;

            var request = new CreatePatientRequest
            {
                Data = patientDto,
            };

            var fakeRepo = new Mock<IPatientRepository>();
            fakeRepo.Setup(x => x.CreatePatientAsync(It.IsAny<Patient>())).Returns(Task.FromResult(expectedId));
            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.CreatePatientAsync(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(expectedId, res.Data.Id);
            Assert.Equal(patientDto.Name, res.Data.Name);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_NameNullException_WhenNameIsNull(string name)
        {
            var patientDto = new PatientDto
            {
                Name = name,
                LastName = "Ciclano",
                CellPhoneNumber = "(16) 99380-2468"
            };

            var request = new CreatePatientRequest
            {
                Data = patientDto,
            };

            var fakeRepo = new Mock<IPatientRepository>();
            fakeRepo.Setup(x => x.CreatePatientAsync(It.IsAny<Patient>())).Returns(Task.FromResult(1));
            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.CreatePatientAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.PATIENT_INVALID_NAME, res.ErrorCode);
            Assert.Equal("Name cannot be null", res.Message);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_LastNameNullException_WhenNameIsNull(string lastName)
        {
            var patientDto = new PatientDto
            {
                Name = "Fulano",
                LastName = lastName,
                CellPhoneNumber = "(16) 99380-2468"
            };

            var request = new CreatePatientRequest
            {
                Data = patientDto,
            };

            var fakeRepo = new Mock<IPatientRepository>();
            fakeRepo.Setup(x => x.CreatePatientAsync(It.IsAny<Patient>())).Returns(Task.FromResult(1));
            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.CreatePatientAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.PATIENT_INVALID_LASTNAME, res.ErrorCode);
            Assert.Equal("Last Name cannot be null", res.Message);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_CellPhoneNumberNullException_WhenCellPhoneNumberIsNull(string cellPhoneNumber)
        {
            var patientDto = new PatientDto
            {
                Name = "Fulano",
                LastName = "Ciclano",
                CellPhoneNumber = cellPhoneNumber
            };

            var request = new CreatePatientRequest
            {
                Data = patientDto,
            };

            var fakeRepo = new Mock<IPatientRepository>();
            fakeRepo.Setup(x => x.CreatePatientAsync(It.IsAny<Patient>())).Returns(Task.FromResult(1));
            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.CreatePatientAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.PATIENT_INVALID_CELLPHONE_NUMBER, res.ErrorCode);
            Assert.Equal("CellPhone cannot be null", res.Message);
        }

        [Fact]
        public async Task Should_Return_PatientNotFound_When_PatientDoesentExist()
        {
            var fakeRepo = new Mock<IPatientRepository>();

            var patient = new Patient();

            fakeRepo.Setup(x => x.GetPatientByIdAsync(333)).Returns(Task.FromResult<Patient>(patient));

            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.GetPatientByIdAsync(333);

            Assert.Null(res.Data);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.PATIENT_NOT_FOUND, res.ErrorCode);
            Assert.Equal("Patient not found.", res.Message);
        }

        [Fact]
        public async Task Should_Return_Patient_Success()
        {
            var fakeRepo = new Mock<IPatientRepository>();

            var fakePatient = new Patient
            {
                Id = 1,
                Name = "Teste",
                LastName = "Teste",
                CellPhoneNumber = "(16) 99380-2468"
            };

            fakeRepo.Setup(x => x.GetPatientByIdAsync(1)).Returns(Task.FromResult((Patient?)fakePatient));

            patientManager = new PatientManager(fakeRepo.Object);

            var res = await patientManager.GetPatientByIdAsync(1);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(fakePatient.Id, res.Data.Id);
            Assert.Equal(fakePatient.Name, res.Data.Name);
        }
    }
}
