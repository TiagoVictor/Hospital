using Application;
using Application.Doctor;
using Application.Doctor.Dto;
using Application.Doctor.Ports;
using Application.Doctor.Requests;
using Domain.Doctor.Entities;
using Domain.Doctor.Ports;
using Moq;

namespace ApplicationTests
{
    public class DoctorManagerTests
    {
        DoctorManager? doctorManager;

        [Fact]
        public async Task HappyPath()
        {
            var doctorDto = new DoctorDto
            {
                Name = "Fulano",
                LastName = "Ciclano",
                Crm = "123123"
            };

            int expectedId = 1;

            var request = new CreateDoctorRequest
            {
                Data = doctorDto,
            };

            var fakeRepo = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateDoctorAsync(It.IsAny<Doctor>())).Returns(Task.FromResult(expectedId));
            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.CreateDoctorAsync(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(expectedId, res.Data.Id);
            Assert.Equal(doctorDto.Name, res.Data.Name);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_NameNullException_WhenNameIsNull(string name)
        {
            var doctorDto = new DoctorDto
            {
                Name = name,
                LastName = "Ciclano",
                Crm = "123123"
            };

            var request = new CreateDoctorRequest
            {
                Data = doctorDto,
            };

            var fakeRepo = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateDoctorAsync(It.IsAny<Doctor>())).Returns(Task.FromResult(1));
            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.CreateDoctorAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.DOCTOR_INVALID_NAME, res.ErrorCode);
            Assert.Equal("Name cannot be null", res.Message);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_LastNameNullException_WhenNameIsNull(string lastName)
        {
            var doctorDto = new DoctorDto
            {
                Name = "Fulano",
                LastName = lastName,
                Crm = "123123"
            };

            var request = new CreateDoctorRequest
            {
                Data = doctorDto,
            };

            var fakeRepo = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateDoctorAsync(It.IsAny<Doctor>())).Returns(Task.FromResult(1));
            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.CreateDoctorAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.DOCTOR_INVALID_LAST_NAME, res.ErrorCode);
            Assert.Equal("Last name cannot be null", res.Message);
        }

        [Theory]
        [InlineData(null)]
        public async Task Should_Return_CrmNullException_WhenNameIsNull(string crm)
        {
            var doctorDto = new DoctorDto
            {
                Name = "Fulano",
                LastName = "Ciclano",
                Crm = crm
            };

            var request = new CreateDoctorRequest
            {
                Data = doctorDto,
            };

            var fakeRepo = new Mock<IDoctorRepository>();
            fakeRepo.Setup(x => x.CreateDoctorAsync(It.IsAny<Doctor>())).Returns(Task.FromResult(1));
            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.CreateDoctorAsync(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.DOCTOR_INVALID_CRM, res.ErrorCode);
            Assert.Equal("Crm cannot be null", res.Message);
        }

        [Fact]
        public async Task Should_Return_DoctorNotFound_When_DoctorDoesentExist()
        {
            var fakeRepo = new Mock<IDoctorRepository>();

            var doctor = new Doctor();

            fakeRepo.Setup(x => x.GetDoctorById(333)).Returns(Task.FromResult<Doctor>(doctor));

            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.GetDoctorByIdAsync(333);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.DOCTOR_NOT_FOUND, res.ErrorCode);
            Assert.Equal("Doctor was not found", res.Message);
        }

        [Fact]
        public async Task Should_Return_Doctor_Success()
        {
            var fakeRepo = new Mock<IDoctorRepository>();

            var fakeDoctor = new Doctor
            {
                Id = 1,
                Name = "Teste",
                LastName = "Teste",
                Crm = "12333"
            };

            fakeRepo.Setup(x => x.GetDoctorById(1)).Returns(Task.FromResult((Doctor?)fakeDoctor));

            doctorManager = new DoctorManager(fakeRepo.Object);

            var res = await doctorManager.GetDoctorByIdAsync(1);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(fakeDoctor.Id, res.Data.Id);
            Assert.Equal(fakeDoctor.Name, res.Data.Name);
        }
    }

}