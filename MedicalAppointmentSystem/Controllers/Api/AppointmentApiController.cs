using MedicalAppointmentSystem.Models.ViewModels;
using MedicalAppointmentSystem.Services;
using MedicalAppointmentSystem.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace MedicalAppointmentSystem.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContentAccesor;
        private readonly string loginUserId;
        private readonly string role;
        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContentAccesor = httpContextAccessor;
            loginUserId = _httpContentAccesor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContentAccesor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        [HttpPost]
        [Route("SaveCalendarData")]

        public IActionResult SaveCalendarData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch (System.Exception e)
            {

                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<AppointmentVM>> commonResponse = new CommonResponse<List<AppointmentVM>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commonResponse.dataenum = _appointmentService.PatientsEventById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else if (role == Helper.Patient)
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else
                {
                    commonResponse.dataenum = _appointmentService.DoctorsEventById(doctorId);
                    commonResponse.status = Helper.success_code;
                }
            }
            catch (System.Exception e)
            {

                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<AppointmentVM> commonResponse = new CommonResponse<AppointmentVM>();
            try
            {
                
                    commonResponse.dataenum = _appointmentService.GetById(id);
                    commonResponse.status = Helper.success_code;
              
            }
            catch (System.Exception e)
            {

                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}
