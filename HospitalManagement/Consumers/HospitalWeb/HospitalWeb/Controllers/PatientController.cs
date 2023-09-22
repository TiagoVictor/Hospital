using Application;
using Application.Patient.Ports;
using Application.Patient.Requests;
using HospitalWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWeb.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientManager _patientManager;

        public PatientController(IPatientManager patientManager)
        {
            _patientManager = patientManager;
        }

        public async Task<IActionResult> Index()
        {
            var patient = await _patientManager.GetPatientsAsync();

            var patientViewModel = new PatientViewModel();

            patient.Patients.ForEach(x => patientViewModel.Models.Add(PatientViewModel.DtoToView(x)));

            return View(patientViewModel);
        }

        public IActionResult CreatePatient()
        {
            return View();
        }

        public async Task<IActionResult> UpdatePatient(int id)
        {
            var patient = await _patientManager.GetPatientByIdAsync(id);

            return View(PatientViewModel.DtoToView(patient.Data));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PatientViewModel patientViewModel)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var request = new CreatePatientRequest
                {
                    Data = PatientViewModel.ViewToDto(patientViewModel)
                };

                var response = await _patientManager.CreatePatientAsync(request);

                if (response.Success) return View("UpdatePatient", PatientViewModel.DtoToView(response.Data));

                return response.ErrorCode switch
                {
                    ErrorCodes.PATIENT_INVALID_NAME => BadRequest(response),
                    ErrorCodes.PATIENT_INVALID_LASTNAME => BadRequest(response),
                    ErrorCodes.PATIENT_INVALID_CELLPHONE_NUMBER => BadRequest(response),
                    ErrorCodes.PATIENT_COULD_NOT_SAVE => BadRequest(response),
                    _ => BadRequest(500)
                };
            }
            else
            {
                return View("CreatePatient", patientViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                var request = new UpdatePatientRequest
                {
                    Id = patientViewModel.Id,
                    Data = PatientViewModel.ViewToDto(patientViewModel)
                };

                _ = await _patientManager.UpdatePatientAsync(request);

                return RedirectToAction("Index");
            }
            else
            {
                return View("UpdatePatient", patientViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientManager.GetPatientByIdAsync(id);
            
            if (patient == null) return NotFound(patient);

            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await _patientManager.DeletePatientAsync(id);
            return RedirectToAction("Index");
        }
    }
}
