using Application.MedicalRecord.Ports;
using Application.MedicalRecord.Requests;
using Application.Patient.Ports;
using HospitalWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWeb.Controllers
{
    [Authorize(Roles = "Doutor")]
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordManager _medicalRecordManager;
        private readonly IPatientManager _patientManager;

        public MedicalRecordController(IMedicalRecordManager medicalRecordManager, IPatientManager patientManager)
        {
            _medicalRecordManager = medicalRecordManager;
            _patientManager = patientManager;
        }

        public async Task<IActionResult> Index()
        {
            var medicalRecord = await _medicalRecordManager.GetMedicalRecordsAsync();

            var medicalRecordViewModel = new MedicalRecordViewModel();

            medicalRecord.MedicalRecords.ForEach(x => medicalRecordViewModel.Models.Add(MedicalRecordViewModel.DtoToView(x)));

            return View(medicalRecordViewModel);
        }

        public async Task<IActionResult> CreateMedicalRecord()
        {
            var patients = await _patientManager.GetPatientsAsync();
            var medicalRecordViewModel = new MedicalRecordViewModel();

            patients.Patients.ForEach(x => medicalRecordViewModel.Patients.Add(PatientViewModel.DtoToView(x)));

            return View(medicalRecordViewModel);
        }

        public async Task<IActionResult> UpdateMedicalRecord(int id)
        {
            var medicalRecord = await _medicalRecordManager.GetMedicalRecordByIdAsync(id);
            var medicalView = MedicalRecordViewModel.DtoToView(medicalRecord.Data);

            var patients = await _patientManager.GetPatientsAsync();
            patients.Patients.ForEach(x => medicalView.Patients.Add(PatientViewModel.DtoToView(x)));

            return View(medicalView);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MedicalRecordViewModel medicalRecordViewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Doctor.Crm");
            ModelState.Remove("Doctor.Name");
            ModelState.Remove("Doctor.LastName");
            ModelState.Remove("Patient.Name");
            ModelState.Remove("Patient.LastName");
            ModelState.Remove("Patient.CellPhoneNumber");
            medicalRecordViewModel.Doctor.Id = Convert.ToInt32(User.FindFirst("Id").Value);

            if (ModelState.IsValid)
            {
                var request = new CreateMedicalRecordRequest
                {
                    Data = MedicalRecordViewModel.ViewToDto(medicalRecordViewModel)
                };

                var response = await _medicalRecordManager.CreateMedicalRecordAsync(request);

                return response.Success ? RedirectToAction("Index") :
                    View("CreateMedicalRecord", medicalRecordViewModel);
            }
            else
            {
                var patients = await _patientManager.GetPatientsAsync();
                patients.Patients.ForEach(x => medicalRecordViewModel.Patients.Add(PatientViewModel.DtoToView(x)));
                return View("CreateMedicalRecord", medicalRecordViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(MedicalRecordViewModel medicalRecordViewModel)
        {
            ModelState.Remove("Doctor.Crm");
            ModelState.Remove("Doctor.Name");
            ModelState.Remove("Doctor.LastName");
            ModelState.Remove("Patient.Name");
            ModelState.Remove("Patient.LastName");
            medicalRecordViewModel.Doctor.Id = Convert.ToInt32(User.FindFirst("Id").Value);
            if (ModelState.IsValid)
            {
                var request = new UpdateMedicalRecordRequest
                {
                    Id = medicalRecordViewModel.Id,
                    Data = MedicalRecordViewModel.ViewToDto(medicalRecordViewModel)
                };

                _ = await _medicalRecordManager.UpdateMedicalRecordAsync(request);

                return RedirectToAction("Index");
            }
            else
            {
                var patients = await _patientManager.GetPatientsAsync();
                patients.Patients.ForEach(x => medicalRecordViewModel.Patients.Add(PatientViewModel.DtoToView(x)));
                return View("UpdateMedicalRecord", medicalRecordViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _medicalRecordManager.DeleteMedicalRecordAsync(id);
            return RedirectToAction("Index");
        }
    }
}
