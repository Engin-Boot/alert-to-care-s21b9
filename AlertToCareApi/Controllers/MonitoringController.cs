
using Microsoft.AspNetCore.Mvc;
using SharedProjects;
using SharedProjects.Models;
using SharedProjects.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        #region Main Functions
        [HttpGet("HealthStatus")]
        public ActionResult<IEnumerable<string>> GetAlarmForAllPatients()
        {
            try
            {
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                var patientStore = _context.Patients.ToList();
                List<Alarm> patientAlarms = new List<Alarm>();
                foreach (Patients patient in patientStore)
                {
                    Alarm patientVitalsAlarms = vitalsMonitoring.GetVitalsForSpecificPatient(patient.PatientId);
                    patientAlarms.Add(patientVitalsAlarms);
                }
                return Ok(patientAlarms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("HealthStatus/{PatientId}")]
        public ActionResult<IEnumerable<string>> GetAlarmForParticularPatient(int patientId)
        {
            try
            {
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                var patientStore = _context.Patients.FirstOrDefault(item => item.PatientId == patientId);
                Debug.Assert(patientStore != null, nameof(patientStore) + " != null");
                var monStat = patientStore.MonitoringStatus;
                if (monStat == 0)
                {
                    Alarm patientVitalsAlarms = vitalsMonitoring.GetVitalsForSpecificPatient(patientId);
                    return Ok(patientVitalsAlarms);
                }
                else
                {
                    string message = "No Alarms : Patient's Monitoring Status is Off ";
                    return BadRequest(message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        //Get All Patient Info
        [HttpGet("PatientInfo")]
        public ActionResult<IEnumerable<Patients>> GetAllPatientInfo()
        {
            try
            {
                var patientStore = _context.Patients.ToList();
                return Ok(patientStore);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //Get Particular Patient Info 
        [HttpGet("PatientInfo/{patientId}")]
        public ActionResult<Patients> GetParticularPatientInfo(int patientId)
        {
            try
            {
                var patientStore = _context.Patients.ToList();
                var patient = patientStore.FirstOrDefault(item => item.PatientId == patientId);
                if (patient == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }
                return Ok(patient);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("SetAlarmOn/{patientId}")]
        public IActionResult SetAlarmOn(int patientId)
        {
            try
            {
                var patientStore = _context.Patients.ToList();
                var patientWithGivenPatientId = patientStore.FirstOrDefault(item => item.PatientId == patientId);
                if (patientWithGivenPatientId == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }
                else
                {
                    patientWithGivenPatientId.MonitoringStatus = 0;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SetAlarmOff/{patientId}")]
        public IActionResult SetAlarmOff(int patientId)
        {
            try
            {
                var patientStore = _context.Patients.ToList();
                var patientWithGivenPatientId = patientStore.FirstOrDefault(item => item.PatientId == patientId);
                if (patientWithGivenPatientId == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }
                else
                {

                    patientWithGivenPatientId.MonitoringStatus = 1;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Manipulation Functions
        [HttpGet("Vitals/{patientid}")]
        public ActionResult<IEnumerable<VitalsLogs>> GetVitalsInfoOfPatient(int patientid)
        {
            var vitalStore = _context.VitalsLogs.ToList();
            var patientVital = vitalStore.FirstOrDefault(item => item.PatientId == patientid);
            try
            {
                if (patientVital == null)
                {
                    return BadRequest("No Vital With The Given Patient ID Exists");
                }
                return Ok(patientVital);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Vitals")]
        public ActionResult<IEnumerable<VitalsLogs>> GetVitalsInfo()
        {
            try
            {
                return Ok(_context.VitalsLogs.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Vitals")]
        public IActionResult AddVitalsInfo([FromBody] VitalsLogs vitals)
        {
            try
            {
                _context.VitalsLogs.Add(vitals);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Vitals/{vitallogId}")]
        public IActionResult UpdateVitalsInfo(int vitallogId, [FromBody] VitalsLogs updatedVitals)
        {
            try
            {
                var vitalStore = _context.VitalsLogs.ToList();
                var vitalToBeUpdated = vitalStore.FirstOrDefault(item => item.VitalsLogId == vitallogId);
                if(vitalToBeUpdated == null)
                {
                    return BadRequest("No Vital With The Given Vital ID Exists");
                }
                _context.Remove(vitalToBeUpdated);
                _context.Add(updatedVitals);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
      
        [HttpDelete("Vitals/{vitallogId}")]
        public IActionResult DeleteVitalsInfo(int vitallogId)
        {
            try
            {
                var vitalStore = _context.VitalsLogs.ToList();
                var vitalToBeDeleted = vitalStore.FirstOrDefault(item => item.VitalsLogId == vitallogId);
                if (vitalToBeDeleted == null)
                {
                    return BadRequest("No Vital With The Given Vital ID Exists");
                }
                _context.Remove(vitalToBeDeleted);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
