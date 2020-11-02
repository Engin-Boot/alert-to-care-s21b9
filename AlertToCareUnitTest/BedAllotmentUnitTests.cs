using System.Linq;
using SharedProjects.Models;
using SharedProjects.Utilities;
using Xunit;

namespace AlertToCareUnitTest
{
    public class BedAllotmentUnitTests
    {
        BedAllotment bedAllotment = new BedAllotment();

        [Fact]

        public void GetAvailabilityOfBeds_ReturnsAListOfAvailaibleBeds()
        {
            int beds = 0;
            
            var availableListOfBeds = bedAllotment.GetAvailableBeds();
            beds += (from bed in availableListOfBeds
                     where !bed.OccupancyStatus
                     select bed).Count();
            Assert.True(beds == availableListOfBeds.Count());
        }

        [Fact]
        public void EmptyTheBed_ShouldUpdateOccupancyStatusToFalse()
        {
            Patients patient = new Patients { PatientName = "Nikita Kumari", BedId = 1, ContactNo = "9826376268", MonitoringStatus = 0, PatientId = 1 };
            
            bedAllotment.EmptyTheBed(patient);
            var allBeds = bedAllotment.GetAvailableBeds();
            foreach (var bed in allBeds)
            {
                Assert.True(bed.BedId == 1);
                break;
            }

        }

        //check
        [Fact]
        public void AllotTheBed_ShouldUpdateStatusWhenBedIdAndStatusIsGiven()
        {
            Patients patient = new Patients { PatientName = "Nikita Kumari", BedId = 1, ContactNo = "9826376268", MonitoringStatus = 0, PatientId = 1 };
            bedAllotment.AllotBedToPatient(patient);
            var allBeds = bedAllotment.GetAvailableBeds();
            foreach (var bed in allBeds)
            {
                Assert.False(bed.BedId == 1);
                break;
            }

        }
    }
}
