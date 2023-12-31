using Application.Doctor;
using Application.Doctor.Ports;
using Application.MedicalRecord;
using Application.MedicalRecord.Ports;
using Application.Patient;
using Application.Patient.Ports;
using Data;
using Data.Doctor;
using Data.MedicalRecord;
using Data.Patient;
using Domain.Doctor.Ports;
using Domain.MedicalRecord.Ports;
using Domain.Patient.Ports;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HospitalDbContext>(
    options => options.UseSqlServer(connectionString));

#region IOC
builder.Services.AddScoped<IPatientManager, PatientManager>();
builder.Services.AddScoped<IDoctorManager, DoctorManager>();
builder.Services.AddScoped<IMedicalRecordManager, MedicalRecordManager>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
#endregion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
