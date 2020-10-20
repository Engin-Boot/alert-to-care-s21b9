﻿using AlertToCareApi.EntriesValidator;
using AlertToCareApi.Models;
using AlertToCareApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlertToCareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupancyController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        #region MainFunctions

        [HttpGet("AvailableBeds")]
        public ActionResult<IEnumerable<Beds>> GetBedsAvailability()
        {
            try
            {
                BedAllotment bedAllotment = new BedAllotment();
                return Ok(bedAllotment.GetAvailableBeds());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Status/{BedId}")]
        public IActionResult GetBedsOccupancyStatus(int bedId)
        {
            try
            {
                var bedStore = _context.Beds.ToList();
                var findBedWithId = bedStore.FirstOrDefault(item => item.BedId == bedId);
                if (findBedWithId == null)
                {
                    return BadRequest("No Bed With The Given Bed Id Exists");
                }
                else
                {
                    var status = findBedWithId.OccupancyStatus;
                    return Ok(status);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("PatientInfo")]
        public IActionResult AddPatientInfo([FromBody] Patients patient)
        {
            try
            {
                BedAllotment bedAllotment = new BedAllotment();
                List<Beds> availableBeds = bedAllotment.GetAvailableBeds();
                bool validInfo = false;
                string message = "";
                PatientInfoValidator.ValidateInfoAndCheckForAvailability(patient, availableBeds.Count, ref validInfo, ref message);
                if (!validInfo)
                {
                    return BadRequest(message);
                }
                else
                {
                    patient.BedId = availableBeds[0].BedId;
                    _context.Patients.Add(patient);
                    _context.SaveChanges();
                    bedAllotment.AllotBedToPatient(patient);
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("PatientVitals")]
        public void AddVitalsForPatient(VitalsLogs vitals)
        {
            _context.VitalsLogs.Add(vitals);
            _context.SaveChanges();
        }

        [HttpDelete("PatientInfo/{patientId}")]
        public IActionResult DischargingPatient(int patientId)
        {
            try
            {
                BedAllotment bedAllotment = new BedAllotment();
                var patientStore = _context.Patients.ToList();
                var patientToBeDischarged = patientStore.FirstOrDefault(item => item.PatientId == patientId);
                if (patientToBeDischarged == null)
                {
                    return BadRequest("No Patient With The Given Patient Id Exists");
                }
                bedAllotment.EmptyTheBed(patientToBeDischarged);
                PatientInfoValidator.DeleteVitalLogsForDischargedPatient(patientId);
                _context.Remove(patientToBeDischarged);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

        #region Manipulation Functions


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
        #endregion

    }
}
