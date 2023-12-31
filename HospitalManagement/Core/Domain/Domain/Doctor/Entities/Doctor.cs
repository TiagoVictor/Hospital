﻿using Domain.Doctor.Exceptions;
using Domain.Doctor.Ports;
using Entity = Domain.MedicalRecord.Entities;

namespace Domain.Doctor.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Crm { get; set; }
        public IList<Entity.MedicalRecord> MedicalRecords { get; set; }

        public void ValidateState()
        {
            if (string.IsNullOrEmpty(Name))
                throw new NameNullException();

            if (string.IsNullOrEmpty(LastName))
                throw new LastNameNullException();

            if (string.IsNullOrEmpty(Crm))
                throw new CrmNullException();
        }

        public async Task Save(IDoctorRepository repository)
        {
            ValidateState();

            if (Id == 0)
                Id = await repository.CreateDoctorAsync(this);
            else
                await repository.UpdateDoctorAsync(this);
        }
    }
}