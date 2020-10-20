﻿using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                List<string> messages = new List<string>();
                VitalsMonitoring vitalsMonitoring = new VitalsMonitoring();
                IEnumerable<VitalsLogs> patientVitals = vitalsMonitoring.GetAllVitals();
                foreach (VitalsLogs vitals in patientVitals)
                {
                    messages.Add(vitalsMonitoring.CheckVitals(vitals));
                }
                return Ok(messages);
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
