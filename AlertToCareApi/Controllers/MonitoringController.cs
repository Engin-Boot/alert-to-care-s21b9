
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedProjects;
using SharedProjects.Models;
using SharedProjects.Utilities;

namespace AlertToCareApi.Controllers
{
   
[Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly static ConfigDbContext Context = new ConfigDbContext();
        static List<Patients> patientStore = Context.Patients.ToList();
       
        
        #region Main Functions
        [HttpGet("HealthStatus")]
        public ActionResult<IEnumerable<string>> GetAlarmForAllPatients()
        {
            try
            {
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
               
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
        public ActionResult<IEnumerable<string>> GetAlarmForParticularPatient(int patientIdToGetHealthStatus)
        {
            try
            {
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                var firstOrDefault = Context.Patients.FirstOrDefault(item => item.PatientId == patientIdToGetHealthStatus);
                Debug.Assert(firstOrDefault != null, nameof(firstOrDefault) + " != null");
                var monStat = firstOrDefault.MonitoringStatus;
                if (monStat == 0)
                {
                    Alarm patientVitalsAlarms = vitalsMonitoring.GetVitalsForSpecificPatient(patientIdToGetHealthStatus);
                    return Ok(patientVitalsAlarms);
                }

                string message = "No Alarms : Patient's Monitoring Status is Off ";
                return BadRequest(message);
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
               
                return Ok(patientStore);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //Get Particular Patient Info 
        [HttpGet("PatientInfo/{patientIdToGetPatientInfo}")]
        public ActionResult<Patients> GetParticularPatientInfo(int patientIdToGetPatientInfo)
        {
            try
            {
               
                var patient = patientStore.FirstOrDefault(item => item.PatientId == patientIdToGetPatientInfo);
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

        [HttpGet("SetAlarmOn/{patientIdToSetAlaramOn}")]
        public IActionResult SetAlarmOn(int patientIdToSetAlaramOn)
        {
            try
            {
                
                var patientWithGivenPatientId = patientStore.FirstOrDefault(item => item.PatientId == patientIdToSetAlaramOn);
                if (patientWithGivenPatientId == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }

                patientWithGivenPatientId.MonitoringStatus = 0;
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SetAlarmOff/{patientIdToSetAlaramOff}")]
        public IActionResult SetAlarmOff(int patientIdToSetAlaramOff)
        {
            try
            {
                Patients patientWithGivenPatientId = patientStore.FirstOrDefault(item => item.PatientId == patientIdToSetAlaramOff);

                if (patientWithGivenPatientId == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }

                patientWithGivenPatientId.MonitoringStatus = 1;
                Context.SaveChanges();
                return Ok();
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
            var vitalStore = Context.VitalsLogs.ToList();
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
                return Ok(Context.VitalsLogs.ToList());
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
                Context.VitalsLogs.Add(vitals);
                Context.SaveChanges();
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
                var vitalStore = Context.VitalsLogs.ToList();
                var vitalToBeUpdated = vitalStore.FirstOrDefault(item => item.VitalsLogId == vitallogId);
                if(vitalToBeUpdated == null)
                {
                    return BadRequest("No Vital With The Given Vital ID Exists");
                }
                Context.Remove(vitalToBeUpdated);
                Context.Add(updatedVitals);
                Context.SaveChanges();
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
                var vitalStore = Context.VitalsLogs.ToList();
                var vitalToBeDeleted = vitalStore.FirstOrDefault(item => item.VitalsLogId == vitallogId);
                if (vitalToBeDeleted == null)
                {
                    return BadRequest("No Vital With The Given Vital ID Exists");
                }
                Context.Remove(vitalToBeDeleted);
                Context.SaveChanges();
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
