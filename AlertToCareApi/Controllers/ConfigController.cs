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
    public class ConfigController : ControllerBase
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        #region MainFunctions
        [HttpGet("Beds")]
        public ActionResult<IEnumerable<Beds>> GetAllBedsInfo()
        {
            try
            {
                return Ok(_context.Beds.ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Icu")]
        public ActionResult<IEnumerable<Icu>> GetAllIcuInfo()
        {
            try
            {
                return Ok(_context.Icu.ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("Floor/{floorno}")]
        public ActionResult<IEnumerable<Icu>> GetAllIcuOnAFloor(int floorNo)
        {
            try
            {
                var icuStore = _context.Icu.ToList();
                List<Icu> icuOnAFloor = new List<Icu>();
                foreach (var icu in icuStore)
                {
                    if (icu.FloorNo == floorNo)
                        icuOnAFloor.Add(icu);
                }
                return Ok(icuOnAFloor);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet("BedsInEachIcu")]
        public ActionResult<IEnumerable<NumberOfBedsInIcu>> GetNumberOfBedsInEachIcu()
        {
            try
            {
                var icuStore = _context.Icu.ToList();
                List<NumberOfBedsInIcu> numberOfBedsInIcu = new List<NumberOfBedsInIcu>();
                BedIdentification bedIdentification = new BedIdentification();
                foreach (Icu icu in icuStore)
                {
                    numberOfBedsInIcu.Add(new NumberOfBedsInIcu { IcuRoomNo = icu.IcuNo, CountOfBeds = bedIdentification.FindCountOfBeds(icu.IcuNo) });
                }
                return Ok(numberOfBedsInIcu);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("BedsInIcu/{IcuNo}")]
        public ActionResult<IEnumerable<Beds>> GetNumberOfBedsInGivenIcu(int icuNo)
        {
            try
            {
                var bedStore = _context.Beds.ToList();
                List<Beds> BedsInIcu = new List<Beds>();
                foreach (var bed in bedStore)
                {
                    if (bed.IcuNo == icuNo)
                        BedsInIcu.Add(bed);
                }
                return Ok(BedsInIcu);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("Beds/{BedId}")]
        public ActionResult<Beds> GetParticularBedInfo(int bedId)
        {
            try
            {
                var bedStore = _context.Beds.ToList();
                var bed = bedStore.FirstOrDefault(item => item.BedId == bedId);
                if (bed == null)
                {
                    return BadRequest("No Bed With The Given Bed Id Is Present");
                }
                return Ok(bed);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("Layouts")]
        public ActionResult<IEnumerable<Layouts>> GetLayoutInfo()
        {
            try
            {
                LayoutInformation layoutInformation = new LayoutInformation();
                var layoutStore = LayoutInformation.Layouts;
                foreach (Layouts layout in layoutStore)
                {
                    layout.NoOfIcus = layoutInformation.FindNoOfIcus(layout.LayoutId);
                    layout.ListOfIcus = layoutInformation.FindListOfIcus(layout.LayoutId);
                }
                return Ok(layoutStore);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion

        #region Manipulation Functions


        [HttpPost("Beds")]
        public IActionResult AddNewBed([FromBody] Beds bed)
        {
            try
            {
                bool validIcu = false;
                string message = "";
                IcuValidator.CheckForValidIcu(bed.IcuNo, ref validIcu, ref message);
                if (!validIcu)
                {
                    return BadRequest(message);
                }
                else
                {
                    BedIdentification bedIdentification = new BedIdentification();
                    bed.BedSerialNo = bedIdentification.FindBedSerialNo(bed.IcuNo);
                    _context.Add(bed);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("AddnewIcu")]
        public IActionResult AddNewIcu([FromBody] Icu icu)
        {
            try
            {
                if (!LayoutValidator.CheckIfLayoutIsPresent(icu.LayoutId))
                {
                    return BadRequest("The Inserted Layout Id For The ICU is Not Available");
                }
                else
                {
                    _context.Add(icu);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
