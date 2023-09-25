using Application.Doctor.Ports;
using Application.Doctor.Requests;
using Application.Patient.Ports;
using Application.Patient.Requests;
using HospitalWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDoctorManager _doctorManager;
        private readonly IPatientManager _patientManager;

        public LoginController(IDoctorManager doctorManager, IPatientManager patientManager)
        {
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PatientRegister()
        {
            return View();
        }
        public IActionResult DoctorRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginViewModel loginViewModel)
        {
            if (loginViewModel.UserType == UserType.Doutor)
            {
                ModelState.Remove("CellPhoneNumber");
                if (ModelState.IsValid)
                {
                    var doctorRequest = new CreateDoctorRequest
                    {
                        Data = LoginViewModel.ViewToDoctorDto(loginViewModel)
                    };

                    await _doctorManager.CreateDoctorAsync(doctorRequest);

                    return View("Index");
                }

                return View("DoctorRegister", loginViewModel);
            }
            else
            {
                ModelState.Remove("Crm");
                if (ModelState.IsValid)
                {
                    var patientRequest = new CreatePatientRequest
                    {
                        Data = LoginViewModel.ViewToPatientDto(loginViewModel)
                    };

                    await _patientManager.CreatePatientAsync(patientRequest);

                    return RedirectToAction("Index");
                }

                return View("PatientRegister", loginViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (loginViewModel.UserType == UserType.Paciente)
                {
                    var patient = await _patientManager.GetPatientByCellPhoneAsync(loginViewModel.CellPhoneNumber);

                    if (patient.Data != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("Id", patient.Data.Id.ToString()),
                            new Claim(ClaimTypes.Name, patient.Data.Name),
                            new Claim(ClaimTypes.Surname, patient.Data.LastName),
                            new Claim(ClaimTypes.Role, "Paciente")
                        };

                        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(userIdentity);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                            IsPersistent = true,
                        };


                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);


                        return RedirectToAction("Index", "Patient");
                    }
                }

                if (loginViewModel.UserType == UserType.Doutor)
                {
                    var doctor = await _doctorManager.GetDoctorByCrmAsync(loginViewModel.Crm);

                    if (doctor.Data != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("Id", doctor.Data.Id.ToString()),
                            new Claim(ClaimTypes.Name, doctor.Data.Name),
                            new Claim(ClaimTypes.Surname, doctor.Data.LastName),
                            new Claim(ClaimTypes.Role, "Doutor")
                        };

                        var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(userIdentity);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                            IsPersistent = true,
                        };


                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);


                        return RedirectToAction("Index", "MedicalRecord");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
