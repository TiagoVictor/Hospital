using Application;
using Application.MedicalRecord.Ports;
using Application.Patient.Ports;
using Application.Patient.Requests;
using HospitalWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWeb.Controllers
{
    [Authorize(Roles = "Paciente")]
    public class PatientController : Controller
    {
        private readonly IPatientManager _patientManager;
        private readonly IMedicalRecordManager _medicalRecordManager;

        public PatientController(IPatientManager patientManager, IMedicalRecordManager medicalRecordManager)
        {
            _patientManager = patientManager;
            _medicalRecordManager = medicalRecordManager;
        }

        public async Task<IActionResult> Index()
        {
            var medicalRecords = await _medicalRecordManager.GetMedicalRecordsByPatientIdAsync(Convert.ToInt32(User.FindFirst("Id").Value));

            var patientViewModel = new PatientViewModel();

            medicalRecords.MedicalRecords.ForEach(x => patientViewModel.MedicalRecords.Add(MedicalRecordViewModel.DtoToView(x)));

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

        public async Task<IActionResult> MedicalRecordReport(int id)
        {
            var medicalRecord = await _medicalRecordManager.GetMedicalRecordByIdAsync(id);
            var medicalView = MedicalRecordViewModel.DtoToView(medicalRecord.Data);

            var patients = await _patientManager.GetPatientsAsync();
            patients.Patients.ForEach(x => medicalView.Patients.Add(PatientViewModel.DtoToView(x)));

            return View(medicalView);
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

                return response.Success ? View("UpdatePatient", PatientViewModel.DtoToView(response.Data)) : 
                    View("CreatePatient", patientViewModel);
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
