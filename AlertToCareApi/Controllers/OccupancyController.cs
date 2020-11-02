using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedProjects;
using SharedProjects.EntriesValidator;
using SharedProjects.Models;
using SharedProjects.Utilities;

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

        [HttpGet("Status/{IcuNo}/{BedId}")]
        public IActionResult GetBedsOccupancyStatus(int icuno, int bedId)
        {
           
                var bedStore = _context.Beds.ToList();
                foreach (var bed in bedStore)
                {
                    if (bed.IcuNo == icuno && bed.BedId == bedId)
                    {
                        return Ok(bed);
                    }
                }
                return BadRequest("No Bed With The Given Bed Id Exists");
            
        }
        #endregion

        #region Manipulation Functions

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

        [HttpPost("Update")]
        public IActionResult UpdateBedInfo([FromBody] Beds updateBed)
        {
            try
            {



                var bedStore = _context.Beds.ToList();
                var bedToBeUpdated =
                    bedStore.FirstOrDefault(item => item.BedId == updateBed.BedId && item.IcuNo == updateBed.IcuNo);
                if (bedToBeUpdated == null)
                {
                    return BadRequest("No Bed With The Given Bed ID Exists");
                }

                _context.Remove(bedToBeUpdated);
                _context.Add(updateBed);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

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

    }
}
