using Application.Doctor.Ports;
using Application.Doctor.Requests;
using HospitalWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWeb.Controllers
{
    [Authorize(Roles = "Doutor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorManager _doctorManager;

        public DoctorController(IDoctorManager doctorManager)
        {
            _doctorManager = doctorManager;
        }

        public async Task<IActionResult> Index()
        {
            var doctor = await _doctorManager.GetDoctorAsync();

            var doctorViewModel = new DoctorViewModel();

            doctor.Doctors.ForEach(x => doctorViewModel.Models.Add(DoctorViewModel.DtoToView(x)));

            return View(doctorViewModel);
        }

        public IActionResult CreateDoctor()
        {
            return View();
        }

        public async Task<IActionResult> UpdateDoctor(int id)
        {
            var doctor = await _doctorManager.GetDoctorByIdAsync(id);
            return View(DoctorViewModel.DtoToView(doctor.Data));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DoctorViewModel doctorViewModel)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var request = new CreateDoctorRequest
                {
                    Data = DoctorViewModel.ViewToDto(doctorViewModel)
                };

                var response = await _doctorManager.CreateDoctorAsync(request);

                return response.Success ? View("UpdateDoctor", DoctorViewModel.DtoToView(response.Data)) :
                    View("CreateDoctor", doctorViewModel);
            }
            else
            {
                return View("CreateDoctor", doctorViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                var request = new UpdateDoctorRequest
                {
                    Data = DoctorViewModel.ViewToDto(doctorViewModel),
                    Id = doctorViewModel.Id
                };

                _ = await _doctorManager.UpdateDoctorAsync(request);

                return RedirectToAction("Index");
            }
            else
            {
                return View("UpdateDoctor", doctorViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _doctorManager.GetDoctorByIdAsync(id);

            if (response.Data == null) return NotFound(response);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            await _doctorManager.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }
    }
}
