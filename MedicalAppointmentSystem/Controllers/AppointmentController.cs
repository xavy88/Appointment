﻿using MedicalAppointmentSystem.Services;
using MedicalAppointmentSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DoctorList = _appointmentService.GetDoctorList();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            ViewBag.SpecialityList = _appointmentService.GetSpecialityList();
            return View();
        }
    }
}
