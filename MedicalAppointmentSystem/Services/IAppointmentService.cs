﻿using MedicalAppointmentSystem.Models;
using MedicalAppointmentSystem.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalAppointmentSystem.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorList();
        public List<PatientVM> GetPatientList();
        public List<Speciality> GetSpecialityList();
        public Task<int> AddUpdate(AppointmentVM model);
        public List<AppointmentVM> DoctorsEventById(string doctorId);
        public List<AppointmentVM> PatientsEventById(string patientId);

        public AppointmentVM GetById(int id);
    }
}

